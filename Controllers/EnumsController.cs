using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Management;
using TV2Presets2.Models;

namespace TV2Presets2.Controllers
{
    public class EnumsController : ApiController
    {
        
        public class EnumRecord
        {
            public string text { get; set; }
            public int value { get; set; }
        }


        [ResponseType(typeof(IEnumerable<EnumRecord>))]
        public IEnumerable<EnumRecord> GetEnum(string enumtype)
        {
            List<EnumRecord> ret = new List<EnumRecord>();

            Type type = Type.GetType("TV2Presets2.Models." + enumtype);

            if (type != null)
            {
                ret.AddRange(Enum.GetValues(type).Cast<int>().Select(en => new EnumRecord { text = Enum.GetName(type, en), value = en }));
            }

            if (enumtype.Equals("BISSTypeEnum"))
                ret = FilterOutBissTypeEnums(ret);

            return ret;
        }

        public string GetEnumText(string enumtype, int val)
        {
            Type type = Type.GetType("TV2Presets2.Models." + enumtype);
            if (type != null)
            {
                return Enum.GetName(type, val);
            }

            return "";

        }

        public List<EnumRecord> FilterOutBissTypeEnums(List<EnumRecord> ret)
        {
            List<EnumRecord> filteredRecords = new List<EnumRecord>(ret.Capacity - 2);
            foreach(EnumRecord record in ret)
            {
                if (record.value == 1 || record.value == 2)
                    continue;

                filteredRecords.Add(record);
            }
            return filteredRecords;
        }
    }
}
