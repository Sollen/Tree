using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace TreeReorder.Models
{
    /// <summary>
    /// Базовый класс дял Ноды
    /// </summary>
    public abstract class Node
    {
        protected Node(string name, int parrentId, NodeType type)
        {
            Name = name;
            ParrentId = parrentId;
            Type = NodeType.File;
            CreateDate = DateTime.Now;
            ModifiDate = DateTime.Now;
            
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ParrentId { get; set; }
        [Required]
        public NodeType Type { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ModifiDate { get; set; }
    }
    /// <summary>
    /// Класс для ноды файла
    /// </summary>
    public class FileNode : Node
    {
        //TODO: Реализовать метод расчёта Размера файла

        public FileNode(string name, int parrentId, string path ="")
            :base(name, parrentId, NodeType.File)
        {
            Size = 1;
            Path = path;
        }
        public Int64 Size { get; set; }
        public string Path { get; set; }
    }
    /// <summary>
    /// Класс для ноды папки
    /// </summary>
    public class FolderNode : Node
    {
        public FolderNode(string name, int parrentId) 
            : base(name, parrentId, NodeType.Folder)
        {
        }
        
    }

    
    public enum NodeType
    {
        File,
        Folder
    }
}
