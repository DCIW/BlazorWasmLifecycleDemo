using BlazorWasmLifecycleDemo.Components;
using BlazorWasmLifecycleDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmLifecycleDemo.ViewModel
{
    public class NotificationsContainer
    {
        public Dictionary<int, ComponentNotification> Notifications { get; } = new Dictionary<int, ComponentNotification>();
        public event Action OnChange;

        private List<(HierarchyLevel HierarchyLevel, string ComponentName)> allPreviouslyRegisteredComponentNames = new List<(HierarchyLevel, string)>();

        private void NotifyStateChanged() => OnChange?.Invoke();

        private int currentPosition = 0;

        public void Add(ComponentNotification notification)
        {
            Notifications.Add(currentPosition, notification);

            if (!allPreviouslyRegisteredComponentNames.Any(c => c.ComponentName.Equals(notification.ComponentName)))
            {
                allPreviouslyRegisteredComponentNames.Add(new(notification.HierarchyLevel, notification.ComponentName));
            }

            currentPosition++;
            NotifyStateChanged();
        }

        public void Clear()
        {
            Notifications.Clear();
            currentPosition = 0;
            NotifyStateChanged();
        }

        public IEnumerable<string> GetComponentNamesFor(HierarchyLevel hierarchyLevel) => allPreviouslyRegisteredComponentNames.Where(c => c.HierarchyLevel.Equals(hierarchyLevel)).OrderBy(c => c.HierarchyLevel).Select(c => c.ComponentName);

        public int GetAmountOfComponentsIn(HierarchyLevel hierarchyLevel) => allPreviouslyRegisteredComponentNames.Count(c => c.HierarchyLevel.Equals(hierarchyLevel));

    }
}
