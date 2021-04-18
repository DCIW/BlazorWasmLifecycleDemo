using BlazorWasmLifecycleDemo.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmLifecycleDemo.Models
{
    public class ComponentNotification
    {
        public string ComponentName { get; set; }

        public string Message { get; set; }

        public HierarchyLevel HierarchyLevel { get; set; }

        public int PositionOnHierarchyLevel { get; set; }

        public ComponentNotification(string componentName, string message, HierarchyLevel hierarchyLevel, int positionOnHierarchyLevel)
        {
            ComponentName = componentName;
            Message = message;
            HierarchyLevel = hierarchyLevel;
            PositionOnHierarchyLevel = positionOnHierarchyLevel;
        }

    }

}
