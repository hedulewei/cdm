using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace study.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]string value)
        {
            try{
            using(var db=new yuukoblogContext()){
                db.Blobs.Add(new Blobs{
                    Id=value,Bytes=new byte[1],ContentLength=3,
                    ContentType="4",FileName="5",Time=DateTime.Now
                });
                db.SaveChanges();
            }
            }
            catch(Exception ex){
                 return "error"+ex.Message;
            }
            return "ok"+value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
