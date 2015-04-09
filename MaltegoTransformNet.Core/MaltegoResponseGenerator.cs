using System;
using System.Collections.Generic;
using MaltegoTransformNet.Core.Enums;
using System.Xml.Serialization;
using System.IO;

namespace MaltegoTransformNet.Core
{
	public class MaltegoResponseGenerator
	{
		public MaltegoMessage MaltegoMessage {
			get;
			set;
		}

		public MaltegoResponseGenerator ()
		{
			MaltegoMessage = new MaltegoMessage();
			MaltegoMessage.MaltegoTransformResponseMessage = new MaltegoTransformResponseMessage();
			MaltegoMessage.MaltegoTransformResponseMessage.Entities = new List<Entity>();
			MaltegoMessage.MaltegoTransformResponseMessage.UIMessages = new List<UIMessage>();

			MaltegoMessage.MaltegoTransformExceptionMessage = new MaltegoTransformExceptionMessage();
			MaltegoMessage.MaltegoTransformExceptionMessage.Exceptions = new List<MaltegoException>();

		}

		/// <summary>
		/// Gets the XML string to be passed back to Maltego.
		/// </summary>
		/// <returns>
		/// The maltego message text.
		/// </returns>
		public string GetMaltegoMessageText ()
		{
			var ser = new XmlSerializer(typeof(MaltegoMessage));
			var writer = new StringWriter();
			ser.Serialize(writer, MaltegoMessage);

			return writer.ToString();
		}

		public override string ToString ()
		{
			return GetMaltegoMessageText();
		}

		/// <summary>
		/// Adds a user interface message to maltego.
		/// </summary>
		/// <param name='messageType'>
		/// Message type.
		/// </param>
		/// <param name='value'>
		/// Value.
		/// </param>
		public void AddUIMessage (UIMessageType messageType, string value)
		{
			MaltegoMessage.MaltegoTransformResponseMessage.UIMessages.Add(new UIMessage {
				MessageType = messageType.ToString (),
				Value = value
			});
		}

		/// <summary>
		/// Adds an affiliation entity.
		/// </summary>
		/// <param name='affiliationType'>
		/// Type of affiliation
		/// </param>
		/// <param name='nickName'>
		/// Nick name.
		/// </param>
		/// <param name='uid'>
		/// Uid. (optional)
		/// </param>
		/// <param name='network'>
		/// Network. (optional)
		/// </param>
		/// <param name='profileUrl'>
		/// Profile URL. (optional)
		/// </param>
		public void AddAffiliationEntity (AffiliationEnum affiliationType, string nickName, string uid = null, string network = null, string profileUrl = null)
		{
			var entity = new Entity();

			entity.Type = affiliationType.ToString ();
			entity.Value = nickName;

			if (!string.IsNullOrWhiteSpace (uid)) {
				entity.AdditionalFields.Add(new Field {
					Name = "uid",
					DisplayName = "uid",
					Value = uid
				});
			}
			if (!string.IsNullOrWhiteSpace (network)) {
				entity.AdditionalFields.Add(new Field {
					Name = "network",
					DisplayName = "network",
					Value = network
				});
			}
			if (!string.IsNullOrWhiteSpace (profileUrl)) {
				entity.AdditionalFields.Add(new Field {
					Name = "profileUrl",
					DisplayName = "profileUrl",
					Value = profileUrl
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);

		}

		/// <summary>
		/// Adds an AS number entity.
		/// </summary>
		/// <param name='ASNumber'>
		/// AS number.
		/// </param>
		public void AddASNumberEntity (string ASNumber)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.ASNumber.ToString();
			entity.Value = ASNumber;

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a DNS name entity.
		/// </summary>
		/// <param name='dnsName'>
		/// Dns name.
		/// </param>
		public void AddDNSNameEntity (string dnsName)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.DNSName.ToString();
			entity.Value = dnsName;

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a document entity.
		/// </summary>
		/// <param name='title'>
		/// Title.
		/// </param>
		/// <param name='link'>
		/// Link. (optional)
		/// </param>
		/// <param name='metainfo'>
		/// Metainfo. (optional)
		/// </param>
		public void AddDocumentEntity (string title, string link = null, string metainfo = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.Document.ToString ();
			entity.Value = title;

			if (!string.IsNullOrWhiteSpace (link)) {
				entity.AdditionalFields.Add (new Field {
					Name = "link",
					DisplayName = "link",
					Value = link
				}
				);
			}

			if (!string.IsNullOrWhiteSpace (metainfo)) {
				entity.AdditionalFields.Add(new Field {
					Name = "metainfo",
					DisplayName = "metainfo",
					Value = metainfo
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a domain entity.
		/// </summary>
		/// <param name='domainName'>
		/// Domain name.
		/// </param>
		/// <param name='whoisInfo'>
		/// Whois info. (optional)
		/// </param>
		public void AddDomainEntity (string domainName, string whoisInfo = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.Domain.ToString ();
			entity.Value = domainName;

			if (!string.IsNullOrWhiteSpace (whoisInfo)) 
			{
				entity.AdditionalFields.Add (new Field 
			    {
					DisplayName = "whois",
					Name = "whois",
					Value = whoisInfo,

				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds an email address entity.
		/// </summary>
		/// <param name='emailAddress'>
		/// Email address.
		/// </param>
		/// <param name='URLS'>
		/// Additional URLs. (optional)
		/// </param>
		public void AddEmailAddressEntity (string emailAddress, List<string> URLS = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.EmailAddress.ToString ();

			if (URLS != null && URLS.Count > 0) {
				entity.AdditionalFields.Add(new Field {
					Name = "URLS",
					DisplayName = "URLS",
					Value = string.Join (Environment.NewLine, URLS)
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds an IP address entity.
		/// </summary>
		/// <param name='ipAddress'>
		/// Ip address.
		/// </param>
		/// <param name='whoIsInfo'>
		/// Who is info.
		/// </param>
		public void AddIPAddressEntity (string ipAddress, string whoIsInfo = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.IPAddress.ToString ();
			entity.Value = ipAddress;

			if (!string.IsNullOrEmpty (whoIsInfo)) {
				entity.AdditionalFields.Add (new Field {
					DisplayName = "whois",
					Name = "whois",
					Value = whoIsInfo
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a location entity.
		/// </summary>
		/// <param name='location'>
		/// Location.
		/// </param>
		/// <param name='longitude'>
		/// Longitude. (optional)
		/// </param>
		/// <param name='latitude'>
		/// Latitude. (optional)
		/// </param>
		/// <param name='country'>
		/// Country. (optional)
		/// </param>
		/// <param name='city'>
		/// City. (optional)
		/// </param>
		/// <param name='area'>
		/// Area. (optional)
		/// </param>
		/// <param name='countrySC'>
		/// Country Short Code. (optional)
		/// </param>
		public void AddLocationEntity (string location, string longitude = null, string latitude = null, string country = null, string city = null, string area = null, string countrySC = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.Location.ToString ();
			entity.Value = location;

			if (!string.IsNullOrWhiteSpace (longitude)) {
				entity.AdditionalFields.Add(new Field {
					Name = "long",
					DisplayName = "long",
					Value = longitude
				});
			}

			if (!string.IsNullOrWhiteSpace (latitude)) {
				entity.AdditionalFields.Add(new Field {
					Name = "lat",
					DisplayName = "lat",
					Value = latitude
				});
			}

			if (!string.IsNullOrWhiteSpace (country)) {
				entity.AdditionalFields.Add(new Field {
					Name = "country",
					DisplayName = "country",
					Value = country
				});
			}

			if (!string.IsNullOrWhiteSpace (city)) {
				entity.AdditionalFields.Add(new Field {
					Name = "city",
					DisplayName = "city",
					Value = city
				});
			}

			if (!string.IsNullOrWhiteSpace (area)) {
				entity.AdditionalFields.Add(new Field {
					Name = "area",
					DisplayName = "area",
					Value = area
				});
			}

			if (!string.IsNullOrWhiteSpace (countrySC)) {
				entity.AdditionalFields.Add(new Field {
					Name = "countrySC",
					DisplayName = "countrySC",
					Value = countrySC
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		
		}

		/// <summary>
		/// Adds an MX record entity.
		/// </summary>
		/// <param name='mxRecord'>
		/// Mx record.
		/// </param>
		public void AddMXRecordEntity (string mxRecord)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.MXrecord.ToString();
			entity.Value = mxRecord;

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a netblock entity.
		/// </summary>
		/// <param name='netBlock'>
		/// Net block.
		/// </param>
		/// <param name='ASNumber'>
		/// AS number. (optional)
		/// </param>
		/// <param name='startIP'>
		/// Start IP Address. (optional)
		/// </param>
		/// <param name='endIP'>
		/// End IP Address. (optional)
		/// </param>
		/// <param name='country'>
		/// Country. (optional)
		/// </param>
		/// <param name='description'>
		/// Description. (optional)
		/// </param>
		public void AddNetblockEntity (string netBlock, string ASNumber = null, string startIP = null, string endIP = null, string country = null, string description = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.Netblock.ToString ();
			entity.Value = netBlock;

			if (!string.IsNullOrWhiteSpace (ASNumber)) {
				entity.AdditionalFields.Add (new Field {
					Name = "ASNumber",
					DisplayName = "ASNumber",
					Value = ASNumber
				}
				);
			}

			if (!string.IsNullOrWhiteSpace (startIP)) {
				entity.AdditionalFields.Add (new Field {
					Name = "startIP",
					DisplayName = "startIP",
					Value = startIP
				}
				);
			}

			if (!string.IsNullOrWhiteSpace (endIP)) {
				entity.AdditionalFields.Add (new Field {
					Name = "endIP",
					DisplayName = "endIP",
					Value = endIP
				}
				);
			}

			if (!string.IsNullOrWhiteSpace (country)) {
				entity.AdditionalFields.Add (new Field {
					Name = "country",
					DisplayName = "country",
					Value = country
				}
				);
			}

			if (!string.IsNullOrWhiteSpace (description)) {
				entity.AdditionalFields.Add(new Field {
					Name = "description",
					DisplayName = "description",
					Value = description
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds an NS record entity.
		/// </summary>
		/// <param name='nsRecord'>
		/// NS record.
		/// </param>
		public void AddNSRecordEntity (string nsRecord)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.NSrecord.ToString();
			entity.Value = nsRecord;

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a person entity.
		/// </summary>
		/// <param name='fullName'>
		/// Full name.
		/// </param>
		/// <param name='firstname'>
		/// Firstname. (optional)
		/// </param>
		/// <param name='lastname'>
		/// Lastname. (optional)
		/// </param>
		/// <param name='additional'>
		/// Additional information. (optional)
		/// </param>
		/// <param name='countrysc'>
		/// Country Short Code. (optional)
		/// </param>
		public void AddPersonEntity (string fullName, string firstname = null, string lastname = null, string additional = null, string countrysc = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.Person.ToString ();
			entity.Value = fullName;

			if (!string.IsNullOrWhiteSpace (firstname)) {
				entity.AdditionalFields.Add(new Field {
					Name = "firstname",
					DisplayName = "firstname",
					Value = firstname
				});
			}

			if (!string.IsNullOrWhiteSpace (lastname)) {
				entity.AdditionalFields.Add(new Field {
					Name = "lastname",
					DisplayName = "lastname",
					Value = lastname
				});
			}

			if (!string.IsNullOrWhiteSpace (additional)) {
				entity.AdditionalFields.Add(new Field {
					Name = "additional",
					DisplayName = "additional",
					Value = additional
				});
			}

			if (!string.IsNullOrWhiteSpace (countrysc)) {
				entity.AdditionalFields.Add(new Field {
					Name = "countrysc",
					DisplayName = "countrysc",
					Value = countrysc
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a phone number entity.
		/// </summary>
		/// <param name='phoneNumber'>
		/// Phone number.
		/// </param>
		/// <param name='countrycode'>
		/// Countrycode. (optional)
		/// </param>
		/// <param name='citycode'>
		/// Citycode. (optional)
		/// </param>
		/// <param name='areacode'>
		/// Areacode. (optional)
		/// </param>
		/// <param name='lastnumbers'>
		/// Lastnumbers. (optional)
		/// </param>
		/// <param name='additional'>
		/// Additional Information. (optional)
		/// </param>
		/// <param name='countrysc'>
		/// Country Short Code. (optional)
		/// </param>
		/// <param name='type'>
		/// Type. (mobile,home,work,etc...) (optional)
		/// </param>
		/// <param name='URLS'>
		/// List of URLs where this phone number was found. (optional)
		/// </param>
		public void AddPhoneNumberEntity (string phoneNumber, string countrycode = null, string citycode = null, string areacode = null, string lastnumbers = null, string additional = null, string countrysc = null, string type = null, List<string> URLS = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.PhoneNumber.ToString ();
			entity.Value = phoneNumber;

			if (!string.IsNullOrWhiteSpace (countrycode)) {
				entity.AdditionalFields.Add(new Field {
					Name = "countrycode",
					DisplayName = "countrycode",
					Value = countrycode
				});
			}

			if (!string.IsNullOrWhiteSpace (citycode)) {
				entity.AdditionalFields.Add(new Field {
					Name = "citycode",
					DisplayName = "citycode",
					Value = citycode
				});
			}

			if (!string.IsNullOrWhiteSpace (areacode)) {
				entity.AdditionalFields.Add(new Field {
					Name = "areacode",
					DisplayName = "areacode",
					Value = areacode
				});
			}

			if (!string.IsNullOrWhiteSpace (lastnumbers)) {
				entity.AdditionalFields.Add(new Field {
					Name = "lastnumbers",
					DisplayName = "lastnumbers",
					Value = lastnumbers
				});
			}

			if (!string.IsNullOrWhiteSpace (additional)) {
				entity.AdditionalFields.Add(new Field {
					Name = "additional",
					DisplayName = "additional",
					Value = additional
				});
			}

			if (!string.IsNullOrWhiteSpace (countrysc)) {
				entity.AdditionalFields.Add(new Field {
					Name = "countrysc",
					DisplayName = "countrysc",
					Value = countrysc
				});
			}

			if (!string.IsNullOrWhiteSpace (type)) {
				entity.AdditionalFields.Add(new Field {
					Name = "type",
					DisplayName = "type",
					Value = type
				});
			}

			if (URLS != null && URLS.Count > 0) {
				entity.AdditionalFields.Add(new Field {
					Name = "URLS",
					DisplayName = "URLS",
					Value = string.Join (Environment.NewLine,URLS)
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a phrase entity.
		/// </summary>
		/// <param name='phrase'>
		/// Phrase.
		/// </param>
		public void AddPhraseEntity (string phrase)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.Phrase.ToString();
			entity.Value = phrase;

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a URL entity.
		/// </summary>
		/// <param name='url'>
		/// URL.
		/// </param>
		/// <param name='fullurl'>
		/// Fullurl. (optional)
		/// </param>
		/// <param name='fulltitle'>
		/// Fulltitle. (optional)
		/// </param>
		public void AddURLEntity (string url, string fullurl = null, string fulltitle = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.URL.ToString ();
			entity.Value = url;

			if (!string.IsNullOrWhiteSpace (fullurl)) {
				entity.AdditionalFields.Add(new Field{
					Name = "fullurl",
					DisplayName = "fullurl",
					Value = fullurl
				});
			}

			if(!string.IsNullOrWhiteSpace(fulltitle)){
				entity.AdditionalFields.Add(new Field {
					Name = "fulltitle",
					DisplayName = "fulltitle",
					Value = fulltitle
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);
		}

		/// <summary>
		/// Adds a website entity.
		/// </summary>
		/// <param name='website'>
		/// Website address.
		/// </param>
		/// <param name='httpsPorts'>
		/// Https ports. (optional)
		/// </param>
		/// <param name='httpPorts'>
		/// Http ports. (optional)
		/// </param>
		/// <param name='servertype'>
		/// Servertype. (optional)
		/// </param>
		/// <param name='URLS'>
		/// List of additional URLs. (optional)
		/// </param>
		public void AddWebsiteEntity (string website, List<int> httpsPorts = null, List<int> httpPorts = null, string servertype = null, List<string> URLS = null)
		{
			var entity = new Entity();

			entity.Type = EntityEnum.Website.ToString ();
			entity.Value = website;

			if (httpsPorts != null && httpsPorts.Count > 0) {
				entity.AdditionalFields.Add (new Field {
					Name = "https",
					DisplayName = "https",
					Value = string.Join (",",httpsPorts)
				}
				);
			}

			if (httpPorts != null && httpPorts.Count > 0) {
				entity.AdditionalFields.Add (new Field{
					Name = "http",
					DisplayName = "http",
					Value = string.Join (",",httpPorts)
				}
				);
			}

			if (URLS != null && URLS.Count > 0) {
				entity.AdditionalFields.Add(new Field{
					Name = "URLS",
					DisplayName = "URLS",
					Value = string.Join ("\r\n", URLS)
				});
			}

			if (!string.IsNullOrWhiteSpace (servertype)) {
				entity.AdditionalFields.Add(new Field{
					Name = "servertype",
					DisplayName = "servertype",
					Value = servertype
				});
			}

			MaltegoMessage.MaltegoTransformResponseMessage.Entities.Add(entity);

		}

	}//End Class


}//End Namespace

