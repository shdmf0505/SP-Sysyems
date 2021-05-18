﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 이 소스 코드가 Microsoft.VSDesigner, 버전 4.0.30319.42000에서 자동으로 생성되었습니다.
// 
#pragma warning disable 1591

namespace Micube.SmartMES.Commons.Controls.SmartDeployService {
    using Micube.Framework;

    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback CallServiceOperationCompleted;
        
        private System.Threading.SendOrPostCallback UploadFileOperationCompleted;

        private System.Threading.SendOrPostCallback DownloadFileOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;

        private string _FileName;
        
        /// <remarks/>
        public WebService() {
            //this.Url = global::Micube.SmartMES.Commons.Controls.Properties.Settings.Default.SmartDeploy_Uploader_SmartDeployService_WebService;
            this.Url = AppConfiguration.GetString("Application.SmartDeploy.Url");
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event CallServiceCompletedEventHandler CallServiceCompleted;
        
        /// <remarks/>
        public event UploadFileCompletedEventHandler UploadFileCompleted;

        /// <remarks/>
        public event DownloadFileCompletedEventHandler DownloadFileCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CallService", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CallService() {
            object[] results = this.Invoke("CallService", new object[0]);
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CallServiceAsync() {
            this.CallServiceAsync(null);
        }
        
        /// <remarks/>
        public void CallServiceAsync(object userState) {
            if ((this.CallServiceOperationCompleted == null)) {
                this.CallServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCallServiceOperationCompleted);
            }
            this.InvokeAsync("CallService", new object[0], this.CallServiceOperationCompleted, userState);
        }
        
        private void OnCallServiceOperationCompleted(object arg) {
            if ((this.CallServiceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CallServiceCompleted(this, new CallServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UploadFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UploadFile([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] f, string filePath, string fileName) {
            object[] results = this.Invoke("UploadFile", new object[] {
                        f,
                        filePath,
                        fileName});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void UploadFileAsync(byte[] f, string filePath, string fileName) {
            this.UploadFileAsync(f, filePath, fileName, null);
        }
        
        /// <remarks/>
        public void UploadFileAsync(byte[] f, string filePath, string fileName, object userState) {
            if ((this.UploadFileOperationCompleted == null)) {
                this.UploadFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadFileOperationCompleted);
            }
            _FileName = fileName;
            this.InvokeAsync("UploadFile", new object[] {
                        f,
                        filePath,
                        fileName}, this.UploadFileOperationCompleted, userState);
        }
        
        private void OnUploadFileOperationCompleted(object arg) {
            if ((this.UploadFileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadFileCompleted(this, new UploadFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState, _FileName));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadFile", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string DownloadFile(string filePath, string fileName, string localPath) {
            object[] results = this.Invoke("DownloadFile", new object[] {
                filePath,
                fileName,
                localPath});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void DownloadFileAsync(string filePath, string fileName, string localPath) {
            this.DownloadFileAsync(filePath, fileName, localPath, null);
        }

        /// <remarks/>
        public void DownloadFileAsync(string filePath, string fileName, string localPath, object userState) {
            if ((this.DownloadFileOperationCompleted == null)) {
                this.DownloadFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadFileOperationCompleted);
            }
            _FileName = fileName;
            this.InvokeAsync("DownloadFile", new object[] {
                filePath,
                fileName,
                localPath}, this.DownloadFileOperationCompleted, userState);
        }

        private void OnDownloadFileOperationCompleted(object arg)
        {
            if ((this.DownloadFileCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadFileCompleted(this, new DownloadFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState, _FileName));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void CallServiceCompletedEventHandler(object sender, CallServiceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CallServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CallServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void UploadFileCompletedEventHandler(object sender, UploadFileCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        private string fileName;
        
        internal UploadFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState, string fileName) : 
                base(exception, cancelled, userState) {
            this.results = results;
            this.fileName = fileName;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void DownloadFileCompletedEventHandler(object sender, DownloadFileCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;
        private string fileName;

        internal DownloadFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState, string fileName) :
            base(exception, cancelled, userState) {
            this.results = results;
            this.fileName = fileName;
        }

        /// <remarks/>
        public string Result {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }
    }
}

#pragma warning restore 1591