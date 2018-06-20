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
        List<Node> Get<T>(T parametr, Func<Node, T, bool> func);
        bool Set<T>(int nodeId, T parametr, Action<Node, T> action);
        bool Insert(Node node);
        bool SetParrent(int nodeId, int parrentId);
    }

    public class EntityHelper:IEntityHelpers
    {
        
        //TODO: Нужно сделать возможность сложного условия.

        ///<summary>
        ///Метод Get для работы с нодами. Возвращает List<Node> по заданному условию.
        ///Возвращает null в случае ошибки
        ///</summary>
        ///<param name="func">Условия для фильрации</param>
        ///<param name="parametr">Параметр для условия фильтрации</param>
        public List<Node> Get<T>(T parametr, Func<Node, T, bool> func)
        {
            try
            {
                using (NodeContext context = new NodeContext())
                {
                    var r = context.Nodes.Where((node) => func(node, parametr)).ToList();
                    return r;
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
        ///Задаёт значение parametr для заданой ноды по заданному условию
        ///Возвращает true в случае успеха. False в случае исключения.
        ///Проверка на наличие ноды в БД. При отсутствии выбразывается исключение.
        ///</summary>
        ///<param name="action">Метод назначения полей</param>
        ///<param name="parametr">Новое значение поля</param>
        ///<param name="nodeId">Id ноды</param>
        public bool Set<T>(int nodeId, T parametr, Action<Node, T> action)
        {
            try
            {
                using (NodeContext context = new NodeContext())
                {
                    var node = context.Nodes.FirstOrDefault(x => x.Id == nodeId);
                    if (node == null) throw new ArgumentNullException();
                    action(node,parametr);
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
        ///<param name="parrentId">Id родителя</param>
        public bool SetParrent(int nodeId, int parrentId)
        {
            try
            {
                using (NodeContext context = new NodeContext())
                {
                    SqlParameter[] param =
                        {
                        new SqlParameter("@nodeid", nodeId),
                        new SqlParameter("@parrentid", parrentId)
                    };

                    var er = context.Nodes.FromSql($"SetParrentId @nodeid, @parrentid", param);
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
