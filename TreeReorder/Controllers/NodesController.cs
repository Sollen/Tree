using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TreeReorder.Helpers;

namespace TreeReorder.Controllers
{
    
    public class NodesController : Controller
    {
        //TODO: Релизовать метод загрузки файлов

        IEntityHelpers entityHelper;

        public NodesController(IEntityHelpers entityHelper)
        {
            this.entityHelper = entityHelper;
            
        }

        ///<summary>
        ///Возвращает все ноды с заданным родителем. (Раскрывает папку)
        ///</summary>
        ///<param name="parentId">Id родителя</param>
        [Route("/GetNodes/{parentId}")]
        [HttpGet]
        public async Task GetNodes(int parentId)
        {            
            await SetResponse(new
            {
                nodes = entityHelper.Get(parentId)
            });

        }

        ///<summary>
        ///Задаёт(изменяет) родителя ноды. (перенос папки\файла в другую папку)
        ///Включена проверка наличия родителя в БД, ниличия дублей родителя и типа родителя как папки.
        ///Если родитель файл, то нода помещается в туже папку, что и этот файл(родитель).
        ///</summary>
        ///<param name="nodeId">Id ноды</param>
        ///<param name="parentId">Id родителя</param>
        [Route("/Setparent/{nodeId}/{parentId}")]
        [HttpGet]
        public IActionResult Setparent(int nodeId, int parentId)
        {

            if (entityHelper.SetParent(nodeId, parentId)) return Ok();
            return StatusCode(501);
        }       

        ///<summary>
        ///Формрование сериализованного ответа на запрос. 
        ///</summary>
        ///<param name="response">Объект для сериализации и отправки в ответе</param>
        private async Task SetResponse(object response)
        {
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }


    }

}







