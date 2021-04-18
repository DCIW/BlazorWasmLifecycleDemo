using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmLifecycleDemo.ViewModel
{
    public class DelayParameterContainer
    {
        public Dictionary<string, DelayParameter> DelayParameters = new Dictionary<string, DelayParameter>();
        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void RegisterComponent(string name)
        {
            if (!DelayParameters.ContainsKey(name))
            {
                DelayParameters.Add(name, new DelayParameter());
                NotifyStateChanged();
            }
        }

        public DelayParameter GetDelayParameterFor(string componentName)
        {
            return DelayParameters[componentName];
        }


    }
}
