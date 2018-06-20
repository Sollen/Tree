using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TreeReorder.Entity;
using TreeReorder.Models;

namespace TreeReorder.Helpers
{
    //TODO: Реализовать передачу сообщений об ошибках

    public interface IEntityHelpers
    {
        List<Node> Get(int parretntId);
        bool Set<T>(int nodeId, int parrentId);
        bool Insert(Node node);
        bool SetParent(int nodeId, int ParentId);
    }

    public class EntityHelper:IEntityHelpers
    {

        //TODO: Нужно сделать возможность сложного условия.

        ///<summary>
        ///Метод Get для работы с нодами. Возвращает List<Node> по Id родителя.
        ///Возвращает null в случае ошибки
        ///</summary>
        ///<param name="parretntId">ID родителя</param>
        public List<Node> Get(int parretntId)
        {
            try
            {
                using (NodeContext context = new NodeContext())
                {
                    return context.Nodes.Where(x => x.parentId == parretntId).ToList(); 
                }
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        //TODO: Нужно сделать возможность изменять несколько полей

        ///<summary>
        ///Метод Set для работы с нодами.
        ///Задаёт значение ID родителя для заданой ноды
        ///Возвращает true в случае успеха. False в случае исключения.
        ///Проверка на наличие ноды в БД. При отсутствии выбразывается исключение.
        ///</summary>
        ///<param name="parrentId">Id родителя</param>
        ///<param name="nodeId">Id ноды</param>
        public bool Set<T>(int nodeId, int parrentId)
        {
            try
            {
                using (NodeContext context = new NodeContext())
                {
                    var node = context.Nodes.FirstOrDefault(x => x.Id == nodeId);
                    if (node == null) throw new ArgumentNullException();
                    node.parentId = parrentId;
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
           
        }

        ///<summary>
        ///Метод устанавливающий родителя. 
        ///Использованная хранимаюв БД процедура        
        ///</summary>
        ///<param name="nodeId">Id ноды для изменения</param>
        ///<param name="ParentId">Id родителя</param>
        public bool SetParent(int nodeId, int ParentId)
        {
            try
            {
                using (NodeContext context = new NodeContext())
                {
                    SqlParameter[] param =
                        {
                        new SqlParameter("@nodeid", nodeId),
                        new SqlParameter("@Parentid", ParentId)
                    };

                    var er = context.Nodes.FromSql($"SetParentId @nodeid, @Parentid", param);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
           
        }

        ///<summary>
        ///Метод Set для работы с нодами.
        ///Задаёт значение parametr для заданой ноды по заданному условию
        ///Возвращает true в случае успеха. False в случае исключения.
        ///Проверка на наличие ноды в БД. При отсутствии выбразывается исключение.
        ///</summary>
        ///<param name="node">Нода дял добавления</param>
        public bool Insert(Node node)
        {
            try
            {
                
                using (NodeContext context = new NodeContext())
                {
                    var search = context.Nodes.Where(n => n.Id == node.Id).ToList();
                    if (search.Count != 0) throw new Exception();
                    context.Nodes.Add(node);
                    context.SaveChanges();
                }
                return true;                
                
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
    }
}
