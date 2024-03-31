using IFC_Editor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.Interfaces;


namespace IFC_Editor.Controllers
{
    internal class ElementController
    {
        public ElementContext Db { get; }

        public ElementController()
        {
            Db = new ElementContext();
        }

        public void SaveDb()
        {
            Db.SaveChanges();
        }

        public void CreateElement(string ElementId, string Name, string ElementType)
        {
            Db.Add(new Element { ElementId = ElementId, Name = Name, ElementType = ElementType, Exclude = false });
        }

        public List<Element> GetAllElements()
        {
            List<Element> elements = Db.Elements.OrderBy(b => b.ElementId).ToList();
            return elements;
        }

        public void DeleteElement(Element Element)
        {
            Db.Remove(Element);
        }

        public string GetElementType(IfcBuildingElement item)
        {
            if (item is IIfcWall wall)
            {
                return "Wall";
            }
            else if (item is IIfcDoor door)
            {
                return "Door";
            }
            else if (item is IIfcWindow window)
            {
                return "Window";
            }
            else if (item is IIfcRoof roof)
            {
                return "Roof";
            }
            else if (item is IIfcFace face)
            {
                return "Face";
            }
            return "Unknown";
        }
    }
}
