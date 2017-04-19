using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CDMservers.Controllers
{
    public class ArchiveController : ApiController
    {
        // GET: api/Archive
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Archive/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Archive
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Archive/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Archive/5
        public void Delete(int id)
        {
        }
    }
}
