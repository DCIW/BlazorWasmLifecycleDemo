using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmLifecycleDemo.ViewModel
{
    public class DelayParameter : INotifyPropertyChanged
    {
        private bool _delaySetParametersAsync;
        public bool DelaySetParametersAsync
        {
            get => _delaySetParametersAsync;
            set
            {
                if (_delaySetParametersAsync != value)
                {
                    _delaySetParametersAsync = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DelaySetParametersAsync)));
                }
            }
        }

        private bool _delayOnParametersSetAsync;
        public bool DelayOnParametersSetAsync
        {
            get => _delayOnParametersSetAsync;
            set
            {
                if (_delayOnParametersSetAsync != value)
                {
                    _delayOnParametersSetAsync = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DelayOnParametersSetAsync)));
                }
            }
        }

        private bool _delayOnInitializedAsync;
        public bool DelayOnInitializedAsync
        {
            get => _delayOnInitializedAsync;
            set
            {
                if (_delayOnInitializedAsync != value)
                {
                    _delayOnInitializedAsync = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DelayOnInitializedAsync)));
                }
            }
        }

        private bool _delayOnAfterRenderAsync;
        public bool DelayOnAfterRenderAsync
        {
            get => _delayOnAfterRenderAsync;
            set
            {
                if (_delayOnAfterRenderAsync != value)
                {
                    _delayOnAfterRenderAsync = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DelayOnAfterRenderAsync)));
                }
            }
        }

        private int _delayTime;
        public int DelayTime
        {
            get => _delayTime;
            set
            {
                if (_delayTime != value)
                {
                    _delayTime = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DelayTime)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
