using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TreeReorder.Helpers;
using TreeReorder.Models;

namespace TreeReorder.Controllers
{
    
    public class NodesController : Controller
    {
        //TODO: Релизовать метод загрузки файлов

        IEntityHelpers entityHelper;

        public NodesController(IEntityHelpers entityHelper)
        {
            this.entityHelper = entityHelper;
            //List<Node> nodes = new List<Node>
            //{
            //    new FileNode("File Node1"),
            //    new FileNode("File Node2"),
            //    new FileNode("File Node3"),
            //    new FileNode("File Node4"),
            //    new FolderNode("Folder Node 1"),
            //    new FolderNode("Folder Node 2"),
            //    new FolderNode("Folder Node 3"),
            //    new FolderNode("Folder Node 4"),
            //};

            //foreach (var node in nodes)
            //{
            //    entityHelper.Insert(node);
            //}
        }

        ///<summary>
        ///Возвращает все ноды с заданным родителем. (Раскрывает папку)
        ///</summary>
        ///<param name="parrentId">Id родителя</param>
        [Route("/GetNodes/{parrentId}")]
        [HttpGet]
        public async Task GetNodes(int parrentId)
        {            
            await SetResponse(new
            {
                nodes = entityHelper.Get<int>(parrentId, (node,parrent) => node.ParrentId == parrent)
            });

        }

        ///<summary>
        ///Задаёт(изменяет) родителя ноды. (перенос папки\файла в другую папку)
        ///Включена проверка наличия родителя в БД, ниличия дублей родителя и типа родителя как папки.
        ///Если родитель файл, то нода помещается в туже папку, что и этот файл(родитель).
        ///</summary>
        ///<param name="nodeId">Id ноды</param>
        ///<param name="nodeId">Id родителя</param>
        [Route("/Setparrent/{nodeId}/{parrentId}")]
        [HttpGet]
        public IActionResult SetParrent(int nodeId, int parrentId)
        {

            if (entityHelper.SetParrent(nodeId, parrentId)) return Ok();
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







