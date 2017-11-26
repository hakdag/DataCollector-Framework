using DataCollector.Common.Contracts;
using DataCollector.IO;
using DataCollector.Planning;
using DataCollector.WebRequest.Get;
using MannDBCollector.Common;
using MannDBCollector.Common.Contracts;
using MannDBCollector.Parsers;
using MannDBCollector.Requestors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MannDBCollector
{
    public class MannDBExecutionPlan : ExecutionPlan<List<VehicleTypeCollectorSet>>//, IMannDBExecutionPlan
    {
        private IInitialPageDocumentRequestor drInitialPage;
        private VehicleTypes? vehicleType;
        private ITitleLinkPair[] vehicleTypes = null;
        private Dictionary<string, ITitleLinkPair[]> vehicleTypeProducers = null;
        private InitialPageParser parserInitialPage;
        private string[] columnStrings;
        private string baseUrl = "http://catalog.mann-filter.com/";

        public MannDBExecutionPlan(IInitialPageDocumentRequestor drInitialPage, string[] columnStrings, VehicleTypes? vehicleType = null)
        {
            this.drInitialPage = drInitialPage;
            this.vehicleType = vehicleType;
            this.parserInitialPage = new InitialPageParser(drInitialPage);
            this.vehicleTypeProducers = new Dictionary<string, ITitleLinkPair[]>();
            this.columnStrings = columnStrings;
        }

        public override void CreatePlan()
        {
            // manndb spesific execution plan...
            // web request - vehicle types
            vehicleTypes = parserInitialPage.Parse();
            if (vehicleType == null)
            {
                createPlanForAllVehicleTypes();
            }
            else
            {
                ITitleLinkPair pair = vehicleTypes[(int)vehicleType];
                createPlanForVehicleType(pair);
            }
        }

        public override List<VehicleTypeCollectorSet> ExecutePlan()
        {
            List<VehicleTypeCollectorSet> vehicleTypes = new List<VehicleTypeCollectorSet>();
            foreach (var item in vehicleTypeProducers)
            {
                VehicleTypeCollectorSet vehicleType = new VehicleTypeCollectorSet();
                vehicleType.Data = item.Key;
                vehicleType.Subsets = getProducers(vehicleType, item);
                vehicleTypes.Add(vehicleType);
            }
            return vehicleTypes;
        }

        private ProducerCollectorSet[] getProducers(VehicleTypeCollectorSet vehicleType, KeyValuePair<string, ITitleLinkPair[]> item)
        {
            List<ProducerCollectorSet> producers = new List<ProducerCollectorSet>();
            foreach (var pair in item.Value)
            {
                ProducerCollectorSet producerSet = new ProducerCollectorSet();
                producerSet.Data = pair.Title;
                producerSet.Parent = vehicleType;
                producerSet.Subsets = getModels(producerSet, pair);
                producers.Add(producerSet);
            }

            return producers.ToArray();
        }

        private ModelCollectorSet[] getModels(ProducerCollectorSet producerSet, ITitleLinkPair producer)
        {
            ModelDocumentRequestor modelDR = new ModelDocumentRequestor(new WebRequestor(), $"{baseUrl}{producer.Link}");
            ModelParser modelParser = new ModelParser(modelDR);
            ITitleLinkPair[] models = modelParser.Parse();

            List<ModelCollectorSet> modelSets = new List<ModelCollectorSet>();
            foreach (var model in models)
            {
                ModelCollectorSet modelSet = new ModelCollectorSet();
                modelSet.Data = model.Title;
                modelSet.Parent = producerSet;
                modelSet.Subsets = getModelPages(modelSet, model);
                modelSets.Add(modelSet);
            }

            return modelSets.ToArray();
        }

        private ModelPageCollectorSet[] getModelPages(ModelCollectorSet modelSet, ITitleLinkPair model)
        {
            ModelPageDocumentRequestor mpDR = new ModelPageDocumentRequestor(new WebRequestor(), $"{baseUrl}{model.Link}");
            ModelPageParser mpParser = new ModelPageParser(mpDR);
            string[] modelPages = mpParser.Parse();

            List<ModelPageCollectorSet> modelPageSets = new List<ModelPageCollectorSet>();
            foreach (var modelPage in modelPages)
            {
                ModelPageCollectorSet modelPageSet = new ModelPageCollectorSet();

                ModelDetailDocumentRequestor mdDR = new ModelDetailDocumentRequestor(new WebRequestor(), $"{baseUrl}{modelPage}");
                ModelDetailParser mdParser = new ModelDetailParser(mdDR);
                ModelPageModel modelPageModel = mdParser.Parse();
                modelPageSet.Data = mdDR;

                modelPageSet.Parent = modelSet;
                modelPageSet.Subsets = getModelDetails(modelPageSet, modelPageModel);
                modelPageSets.Add(modelPageSet);
            }

            return modelPageSets.ToArray();
        }

        private ModelPageDetailCollectorSet[] getModelDetails(ModelPageCollectorSet modelPageSet, ModelPageModel modelPageModel)
        {
            ModelPageDetailCollectorSet modelPageDetailSet = new ModelPageDetailCollectorSet();
            modelPageDetailSet.Parent = modelPageSet;
            modelPageDetailSet.Data = getRowModel(modelPageModel, modelPageSet);

            return new[] { modelPageDetailSet };
        }

        private RowModel getRowModel(ModelPageModel modelPageModel, ModelPageCollectorSet modelPageSet)
        {
            ModelCollectorSet modelSet = (ModelCollectorSet)modelPageSet.Parent;
            ProducerCollectorSet producerSet = (ProducerCollectorSet)modelSet.Parent;
            VehicleTypeCollectorSet vehicleType = (VehicleTypeCollectorSet)producerSet.Parent;

            RowModel rm = new RowModel();
            rm.VehicleType = vehicleType.Data;
            rm.Producer = producerSet.Data;
            rm.Model = modelSet.Data;
            rm.ModelType = modelPageModel.ModelType;
            rm.MotorCode = modelPageModel.MotorCode;
            rm.kW = modelPageModel.kW;
            rm.PS = modelPageModel.PS;
            rm.Year = modelPageModel.Year;

            addFilters(modelPageSet, rm);

            return rm;
        }

        private void addFilters(ModelPageCollectorSet modelPageSet, RowModel rm)
        {
            ModelDetailDocumentRequestor mdDR = (ModelDetailDocumentRequestor)modelPageSet.Data;
            FilterNodeModel fnModelAir = addFilterInfo(mdDR, FilterTypes.Air);
            if (!String.IsNullOrEmpty(fnModelAir.Filters))
            {
                rm.AirFilters = fnModelAir.Filters;
                rm.AirFilterDimensions = addFilterDimensions(fnModelAir.FilterUrls, FilterTypes.Air);
            }

            FilterNodeModel fnModelOil = addFilterInfo(mdDR, FilterTypes.Oil);
            if (!String.IsNullOrEmpty(fnModelOil.Filters))
            {
                rm.OilFilters = fnModelOil.Filters;
                rm.OilFilterDimensions = addFilterDimensions(fnModelOil.FilterUrls, FilterTypes.Oil);
            }

            FilterNodeModel fnModelFuel = addFilterInfo(mdDR, FilterTypes.Fuel);
            if (!String.IsNullOrEmpty(fnModelFuel.Filters))
            {
                rm.FuelFilters = fnModelFuel.Filters;
                rm.FuelFilterDimensions = addFilterDimensions(fnModelFuel.FilterUrls, FilterTypes.Fuel);
            }

            FilterNodeModel fnModelInterior = addFilterInfo(mdDR, FilterTypes.Interior);
            if (!String.IsNullOrEmpty(fnModelInterior.Filters))
            {
                rm.InteriorFilters = fnModelInterior.Filters;
                rm.InteriorFilterDimensions = addFilterDimensions(fnModelInterior.FilterUrls, FilterTypes.Interior);
            }

            FilterNodeModel fnModelOther = addFilterInfo(mdDR, FilterTypes.Other);
            if (!String.IsNullOrEmpty(fnModelOther.Filters))
            {
                rm.OtherFilters = fnModelOther.Filters;
                rm.OtherFilterDimensions = addFilterDimensions(fnModelOther.FilterUrls, FilterTypes.Other);
            }
        }

        /*
        public override DataSet ExecutePlan()
        {
            DataSet ds = new DataSet("MannDB");

            foreach (var item in vehicleTypeProducers)
            {
                DataTable dt = new DataTable(item.Key);

                foreach (var columnString in columnStrings)
                {
                    dt.Columns.Add(columnString, typeof(string));
                }

                foreach (var producer in item.Value)
                {
                    ModelDocumentRequestor modelDR = new ModelDocumentRequestor($"{baseUrl}{producer.Link}");
                    ModelParser modelParser = new ModelParser(modelDR);
                    ITitleLinkPair[] models = modelParser.Parse();

                    foreach (var model in models)
                    {
                        ModelPageDocumentRequestor mpDR = new ModelPageDocumentRequestor($"{baseUrl}{model.Link}");
                        ModelPageParser mpParser = new ModelPageParser(mpDR);
                        string[] modelPages = mpParser.Parse();

                        foreach (var modelPage in modelPages)
                        {
                            ModelDetailDocumentRequestor mdDR = new ModelDetailDocumentRequestor($"{baseUrl}{modelPage}");
                            ModelDetailParser mdParser = new ModelDetailParser(mdDR);
                            ModelPageModel modelPageModel = mdParser.Parse();

                            RowModel rm = new RowModel();
                            rm.VehicleType = item.Key;
                            rm.Producer = producer.Title;
                            rm.Model = model.Title;
                            rm.ModelType = modelPageModel.ModelType;
                            rm.MotorCode = modelPageModel.MotorCode;
                            rm.kW = modelPageModel.kW;
                            rm.PS = modelPageModel.PS;
                            rm.Year = modelPageModel.Year;

                            FilterNodeModel fnModelAir = addFilterInfo(mdDR, FilterTypes.Air);
                            if (!String.IsNullOrEmpty(fnModelAir.Filters))
                            {
                                rm.AirFilters = fnModelAir.Filters;
                                rm.AirFilterDimensions = addFilterDimensions(fnModelAir.FilterUrls, FilterTypes.Air);
                            }

                            FilterNodeModel fnModelOil = addFilterInfo(mdDR, FilterTypes.Oil);
                            if (!String.IsNullOrEmpty(fnModelOil.Filters))
                            {
                                rm.OilFilters = fnModelOil.Filters;
                                rm.OilFilterDimensions = addFilterDimensions(fnModelOil.FilterUrls, FilterTypes.Oil);
                            }

                            FilterNodeModel fnModelFuel = addFilterInfo(mdDR, FilterTypes.Fuel);
                            if (!String.IsNullOrEmpty(fnModelFuel.Filters))
                            {
                                rm.FuelFilters = fnModelFuel.Filters;
                                rm.FuelFilterDimensions = addFilterDimensions(fnModelFuel.FilterUrls, FilterTypes.Fuel);
                            }

                            FilterNodeModel fnModelInterior = addFilterInfo(mdDR, FilterTypes.Interior);
                            if (!String.IsNullOrEmpty(fnModelInterior.Filters))
                            {
                                rm.InteriorFilters = fnModelInterior.Filters;
                                rm.InteriorFilterDimensions = addFilterDimensions(fnModelInterior.FilterUrls, FilterTypes.Interior);
                            }

                            FilterNodeModel fnModelOther = addFilterInfo(mdDR, FilterTypes.Other);
                            if (!String.IsNullOrEmpty(fnModelOther.Filters))
                            {
                                rm.OtherFilters = fnModelOther.Filters;
                                rm.OtherFilterDimensions = addFilterDimensions(fnModelOther.FilterUrls, FilterTypes.Other);
                            }

                            DataRow row = convertRowModelToRow(dt, rm);
                            dt.Rows.Add(row);
                        }
                    }
                }

                ds.Tables.Add(dt);
            }

            return ds;
        }
        */
        private DataRow convertRowModelToRow(DataTable dt, RowModel rm)
        {
            DataRow row = dt.NewRow();
            row[0] = rm.VehicleType;
            row[1] = rm.Producer;
            row[2] = rm.Model;
            row[3] = rm.ModelType;
            row[4] = rm.MotorCode;
            row[5] = rm.kW;
            row[6] = rm.PS;
            row[7] = rm.Year;
            row[8] = rm.AirFilters;
            row[9] = rm.AirFilterDimensions;
            row[10] = rm.OilFilters;
            row[11] = rm.OilFilterDimensions;
            row[12] = rm.FuelFilters;
            row[13] = rm.FuelFilterDimensions;
            row[14] = rm.InteriorFilters;
            row[15] = rm.InteriorFilterDimensions;
            row[16] = rm.OtherFilters;
            row[17] = rm.OtherFilterDimensions;

            return row;
        }

        private string addFilterDimensions(List<string> filterUrls, FilterTypes filterType)
        {
            StringBuilder sb = new StringBuilder();
            List<string> filterDimensions = new List<string>();
            foreach (string filterUrl in filterUrls)
            {
                FilterPageDocumentRequestor fpDR = new FilterPageDocumentRequestor(new WebRequestor(), $"{baseUrl}{filterUrl}");
                FilterPageParser fpParser = new FilterPageParser(fpDR);
                filterDimensions.Add(fpParser.Parse());
            }
            return String.Join("\n", filterDimensions);
        }

        private FilterNodeModel addFilterInfo(ModelDetailDocumentRequestor mdDR, FilterTypes filterType)
        {
            FilterNodeParser fnParser = new FilterNodeParser(mdDR, filterType);
            FilterNodeModel fnModel = fnParser.Parse();
            return fnModel;
        }

        public override void LoadPlan(string filename)
        {
            PlanFile pf = new PlanFile();
            pf.Load<Dictionary<string, ITitleLinkPair[]>>(filename);
            vehicleTypes = pf.GetContent<ITitleLinkPair[]>();
            vehicleTypeProducers = pf.GetContent<Dictionary<string, ITitleLinkPair[]>>();
        }

        public override void SavePlan()
        {
            PlanFile pf = new PlanFile();
            pf.AddContent(vehicleTypes);
            pf.AddContent(vehicleTypeProducers);
            pf.Save("mandb.plan");
        }

        private void createPlanForAllVehicleTypes()
        {
            foreach (ITitleLinkPair pair in vehicleTypes)
            {
                createPlanForVehicleType(pair);
            }
        }

        private void createPlanForVehicleType(ITitleLinkPair vehicleType)
        {
            // web request - producers
            VehicleTypeDocumentRequestor dr = new VehicleTypeDocumentRequestor(new WebRequestor(), vehicleType.Link);
            VehicleTypeParser parser = new VehicleTypeParser(dr);
            ITitleLinkPair[] producers = parser.Parse();
            vehicleTypeProducers.Add(vehicleType.Title, producers);
        }

        public ITitleLinkPair[] VehicleTypes
        {
            get
            {
                return vehicleTypes;
            }
        }

        public Dictionary<string, ITitleLinkPair[]> VehicleTypeProducers
        {
            get
            {
                return vehicleTypeProducers;
            }
        }
    }
}
