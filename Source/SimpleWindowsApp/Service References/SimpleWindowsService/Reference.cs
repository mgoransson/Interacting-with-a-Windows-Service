﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimpleWindowsApp.SimpleWindowsService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Microsoft.ServiceModel.Samples", ConfigurationName="SimpleWindowsService.ISettingsService")]
    public interface ISettingsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Microsoft.ServiceModel.Samples/ISettingsService/GetOutputMessage", ReplyAction="http://Microsoft.ServiceModel.Samples/ISettingsService/GetOutputMessageResponse")]
        string GetOutputMessage();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Microsoft.ServiceModel.Samples/ISettingsService/GetOutputMessage", ReplyAction="http://Microsoft.ServiceModel.Samples/ISettingsService/GetOutputMessageResponse")]
        System.Threading.Tasks.Task<string> GetOutputMessageAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Microsoft.ServiceModel.Samples/ISettingsService/SetOutputMessage", ReplyAction="http://Microsoft.ServiceModel.Samples/ISettingsService/SetOutputMessageResponse")]
        void SetOutputMessage(string outputMessage);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Microsoft.ServiceModel.Samples/ISettingsService/SetOutputMessage", ReplyAction="http://Microsoft.ServiceModel.Samples/ISettingsService/SetOutputMessageResponse")]
        System.Threading.Tasks.Task SetOutputMessageAsync(string outputMessage);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISettingsServiceChannel : SimpleWindowsApp.SimpleWindowsService.ISettingsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SettingsServiceClient : System.ServiceModel.ClientBase<SimpleWindowsApp.SimpleWindowsService.ISettingsService>, SimpleWindowsApp.SimpleWindowsService.ISettingsService {
        
        public SettingsServiceClient() {
        }
        
        public SettingsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SettingsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SettingsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SettingsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetOutputMessage() {
            return base.Channel.GetOutputMessage();
        }
        
        public System.Threading.Tasks.Task<string> GetOutputMessageAsync() {
            return base.Channel.GetOutputMessageAsync();
        }
        
        public void SetOutputMessage(string outputMessage) {
            base.Channel.SetOutputMessage(outputMessage);
        }
        
        public System.Threading.Tasks.Task SetOutputMessageAsync(string outputMessage) {
            return base.Channel.SetOutputMessageAsync(outputMessage);
        }
    }
}
