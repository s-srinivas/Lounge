using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Lounge.Core.Domain
{
    public class ViewResult<T> 
    {
        private List<ViewResultRow<T>> rows = new List<ViewResultRow<T>>();

        [JsonProperty("total_rows")]
        public int RowCount { get; set; }
        [JsonProperty("rows")]
        public ViewResultRow<T>[] Rows 
        { 
            get { return rows.ToArray(); }
            set { rows = value.ToList(); }
        }

        public bool HasRows()
        {
            return Rows.Any();
        }
    }
}