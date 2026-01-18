using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GrantApp.Controllers
{
    public class GrantReportsController : ApiController
    {
        // GET: api/GrantReports
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GrantReports/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GrantReports
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GrantReports/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GrantReports/5
        public void Delete(int id)
        {
        }
    }
}
