using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

/// <summary>
/// Encapsulates a Contact of a User of a Repository which is sent to the rich client application
/// </summary>
[DataContract]
public struct Contact
{
    /// <summary>
    /// Name of the Contact
    /// </summary>    
    [DataMember]
    public string ContactName;

    /// <summary>
    /// Contact's mobile number as string
    /// </summary>    
    [DataMember]
    public string ContactMobile;

    /// <summary>
    /// Contact's phone number as string
    /// </summary>    
    [DataMember]
    public string ContactPhoneNumber;

    /// <summary>
    /// Valid email as string
    /// </summary>    
    [DataMember]
    public string ContactEmail;

    /// <summary>
    /// Tags associated with the Contact
    /// </summary>    
    [DataMember]
    public string Tags;
}