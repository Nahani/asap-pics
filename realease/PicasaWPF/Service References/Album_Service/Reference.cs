﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18034
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PicasaWPF.Album_Service {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AlbumsResponse", Namespace="http://schemas.datacontract.org/2004/07/PicasaServices")]
    [System.SerializableAttribute()]
    public partial class AlbumsResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private PicasaWPF.Album_Service.Album[] AlbumsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public PicasaWPF.Album_Service.Album[] Albums {
            get {
                return this.AlbumsField;
            }
            set {
                if ((object.ReferenceEquals(this.AlbumsField, value) != true)) {
                    this.AlbumsField = value;
                    this.RaisePropertyChanged("Albums");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Album", Namespace="http://schemas.datacontract.org/2004/07/DB")]
    [System.SerializableAttribute()]
    public partial class Album : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int idUserField;
        
        private string nameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int idUser {
            get {
                return this.idUserField;
            }
            set {
                if ((this.idUserField.Equals(value) != true)) {
                    this.idUserField = value;
                    this.RaisePropertyChanged("idUser");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                if ((object.ReferenceEquals(this.nameField, value) != true)) {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Album_Service.IAlbumService")]
    public interface IAlbumService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlbumService/Add", ReplyAction="http://tempuri.org/IAlbumService/AddResponse")]
        bool Add(string name, int idProp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlbumService/Delete", ReplyAction="http://tempuri.org/IAlbumService/DeleteResponse")]
        bool Delete(string name, int idProp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlbumService/Get_Albums_From_User", ReplyAction="http://tempuri.org/IAlbumService/Get_Albums_From_UserResponse")]
        PicasaWPF.Album_Service.AlbumsResponse Get_Albums_From_User(int idProp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlbumService/Get_Albums_From_Other_Users", ReplyAction="http://tempuri.org/IAlbumService/Get_Albums_From_Other_UsersResponse")]
        PicasaWPF.Album_Service.AlbumsResponse Get_Albums_From_Other_Users(int idProp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAlbumService/Get_Album_ID", ReplyAction="http://tempuri.org/IAlbumService/Get_Album_IDResponse")]
        int Get_Album_ID(string name, int idProp);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAlbumServiceChannel : PicasaWPF.Album_Service.IAlbumService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AlbumServiceClient : System.ServiceModel.ClientBase<PicasaWPF.Album_Service.IAlbumService>, PicasaWPF.Album_Service.IAlbumService {
        
        public AlbumServiceClient() {
        }
        
        public AlbumServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AlbumServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AlbumServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AlbumServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Add(string name, int idProp) {
            return base.Channel.Add(name, idProp);
        }
        
        public bool Delete(string name, int idProp) {
            return base.Channel.Delete(name, idProp);
        }
        
        public PicasaWPF.Album_Service.AlbumsResponse Get_Albums_From_User(int idProp) {
            return base.Channel.Get_Albums_From_User(idProp);
        }
        
        public PicasaWPF.Album_Service.AlbumsResponse Get_Albums_From_Other_Users(int idProp) {
            return base.Channel.Get_Albums_From_Other_Users(idProp);
        }
        
        public int Get_Album_ID(string name, int idProp) {
            return base.Channel.Get_Album_ID(name, idProp);
        }
    }
}
