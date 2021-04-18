using BlazorWasmLifecycleDemo.Models;
using BlazorWasmLifecycleDemo.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorWasmLifecycleDemo.Components
{
    public abstract class NotificationComponentBase : ComponentBase
    {
        [Inject]
        private NotificationsContainer messagesContainer { get; set; }

        [Inject]
        private DelayParameterContainer delayParamContainer { get; set; }

        protected abstract string ComponentName { get; }

        private int _currentRenderingRound;
        protected int CurrentRenderingRound
        {
            get => _currentRenderingRound;
            set
            {
                _currentRenderingRound = value;
                CreateNotification($"Set round to = {_currentRenderingRound}");
            }
        }
        private DelayParameter currentDelayParameters;

        protected abstract HierarchyLevel HierarchyLevel { get; }

        protected abstract int PositionOnHierarchyLevel { get; }

        protected void CreateMethodStartedNotification(Color methodColor, [CallerMemberName] string methodname = default)
        {
            string color = ColorTranslator.ToHtml(methodColor);

            string message = $"<span style=\"Color: {color}\" >{methodname}</span> <b>called</b>. Started on rendering round { CurrentRenderingRound }";

            messagesContainer.Add(new ComponentNotification(ComponentName, message, HierarchyLevel, PositionOnHierarchyLevel));
        }

        protected void CreateMethodEndsNotification(int localRenderingRound, Color methodColor, [CallerMemberName] string methodname = default)
        {
            string color = ColorTranslator.ToHtml(methodColor);

            string message = $"<span style=\"Color: {color}\" >{methodname}</span> <b>ends</b>. Started on rendering round { localRenderingRound }. Current round = { CurrentRenderingRound }";

            messagesContainer.Add(new ComponentNotification(ComponentName, message, HierarchyLevel, PositionOnHierarchyLevel));
        }

        protected void CreateParameterChangedNotification([CallerMemberName] string propertyName = default)
        {
            string message = $"{propertyName} changed";

            messagesContainer.Add(new ComponentNotification(ComponentName, message, HierarchyLevel, PositionOnHierarchyLevel));
        }

        protected void CreateNotification(string message)
        {
            messagesContainer.Add(new ComponentNotification(ComponentName, message, HierarchyLevel, PositionOnHierarchyLevel));
        }

        public async override Task SetParametersAsync(ParameterView parameters)
        {
            int round = CurrentRenderingRound;

            if (delayParamContainer.DelayParameters.ContainsKey(ComponentName))
                currentDelayParameters = delayParamContainer.GetDelayParameterFor(ComponentName);


            CreateMethodStartedNotification(Color.Red);
            if (currentDelayParameters?.DelaySetParametersAsync ?? false)
                await Task.Delay(currentDelayParameters.DelayTime);
            await base.SetParametersAsync(parameters);
            CreateMethodEndsNotification(round, Color.Red);
        }

        protected override void OnInitialized()
        {
            delayParamContainer.RegisterComponent(ComponentName);
            int round = CurrentRenderingRound;
            CreateMethodStartedNotification(Color.Green);

            base.OnInitialized();
            CreateMethodEndsNotification(round, Color.Green);
        }

        protected async override Task OnInitializedAsync()
        {
            int round = CurrentRenderingRound;
            CreateMethodStartedNotification(Color.DarkGreen);

            if (currentDelayParameters?.DelayOnInitializedAsync ?? false)
                await Task.Delay(currentDelayParameters.DelayTime);
            await base.OnInitializedAsync();
            CreateMethodEndsNotification(round, Color.DarkGreen);

        }

        protected override void OnParametersSet()
        {
            int round = CurrentRenderingRound;
            CreateMethodStartedNotification(Color.Blue);

            base.OnParametersSet();
            CreateMethodEndsNotification(round, Color.Blue);

        }

        protected async override Task OnParametersSetAsync()
        {
            int round = CurrentRenderingRound;
            CreateMethodStartedNotification(Color.DarkBlue);
            if (currentDelayParameters?.DelayOnParametersSetAsync ?? false)
                await Task.Delay(currentDelayParameters.DelayTime);
            await base.OnParametersSetAsync();
            CreateMethodEndsNotification(round, Color.DarkBlue);

        }

        protected override void OnAfterRender(bool firstRender)
        {
            int round = CurrentRenderingRound;
            CreateMethodStartedNotification(Color.Violet);

            base.OnAfterRender(firstRender);
            CreateMethodEndsNotification(round, Color.Violet);

        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            int round = CurrentRenderingRound;
            CreateMethodStartedNotification(Color.DarkViolet);
            if (currentDelayParameters?.DelayOnAfterRenderAsync ?? false)
                await Task.Delay(currentDelayParameters.DelayTime);
            await base.OnAfterRenderAsync(firstRender);
            CreateMethodEndsNotification(round, Color.DarkViolet);

            if (firstRender)
            {
                currentDelayParameters = delayParamContainer.GetDelayParameterFor(ComponentName);
                currentDelayParameters.PropertyChanged += DelayParameterChanged;
            }

            CurrentRenderingRound++;
        }

        private void DelayParameterChanged(object sender, PropertyChangedEventArgs e)
        {
            currentDelayParameters = delayParamContainer.GetDelayParameterFor(ComponentName);
        }

    }

    public enum HierarchyLevel
    {
        Parent,
        Child,
        Grandchild
    }

}
