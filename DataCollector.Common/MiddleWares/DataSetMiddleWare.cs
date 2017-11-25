using DataCollector.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Common.MiddleWares
{
    public class DataSetMiddleWare<T> : IMiddleWare<T>
    {
        private DataSet ds;

        public DataSetMiddleWare(string name)
        {
            this.Name = name;
            this.ds = new DataSet(name);
        }

        public string Name { get; set; }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void AddGroup(string groupName)
        {
            this.ds.Tables.Add(new DataTable(groupName));
        }

        public void Deserialize()
        {
            throw new NotImplementedException();
        }

        public void Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
