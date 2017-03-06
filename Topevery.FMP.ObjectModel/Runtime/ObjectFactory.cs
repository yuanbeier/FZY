using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Topevery.FMP.ObjectModel.Runtime
{
	/// <summary>
	/// ObjectFactory 的摘要说明。
	/// </summary>
	public static class ObjectFactory
	{
		#region Methods

		#region StringToObject Helper Funcations And Type Helper Functions.
		public static Type GetType(string typeName)
		{
			Type type = null;
			if(!IsEmpty(typeName))
			{
				string[] typeArray = typeName.Split(',');
				if(typeArray != null)
				{
					
					int len = typeArray.Length;
					string asmName = string.Empty;
					if(len == 2)
					{
						typeName = typeArray[0];
						asmName = typeArray[1];
						
					}
					else if(len == 5)
					{
						typeName = typeArray[0];
						for(int i = 1; i < 5; i++)
						{
							if(i > 1)
							{
								asmName +=",";
							}
							asmName += typeArray[i].Trim();
						}
					}
					if(!IsEmpty(asmName))
					{
						asmName = asmName.Trim();
						Assembly asm = Assembly.Load(asmName);
						if(asm != null && !IsEmpty(typeName))
						{
							typeName = typeName.Trim();
							type = asm.GetType(typeName, false, true);
						}
					}
					else if(!IsEmpty(typeName))
					{
						typeName= typeName.Trim();
						type = Type.GetType(typeName, false, true);
					}
				}
			}
			return type;
		}

		public static string GetInstanceTypeName(Type type)
		{
			string result = null;
			if(type != null)
			{
				result = type.FullName;
				result += "," + type.Assembly.GetName().Name;
			}
			return result;
		}

		public static string GetInstanceTypeName(object obj)
		{
			string result = null;
			Type type = null;
			if(obj != null)
			{
				type = obj.GetType();
				result = GetInstanceTypeName(type);
			}

			return result;
		}

		
		public static string ToString(object value)
		{
			string result = null;
			if(value != null)
			{
				Type type = value.GetType();
				if(type == typeof(string))
				{
					result = (string)value;
				}
				else if(type == typeof(byte[]))
				{
					result = ByteArray2String(value as byte[]);
				}
				else
				{
					TypeConverter  convert = TypeDescriptor.GetConverter(type) ;
					if(convert != null && convert.CanConvertFrom(typeof(string))
						&& convert.CanConvertTo(typeof(string)))
					{
						result = convert.ConvertToString(value);
					}
					else 
					{
						MemoryStream inStream = new MemoryStream();
						IFormatter formatter = new BinaryFormatter();
						try
						{
							formatter.Serialize(inStream, value);
							result = ByteArray2String(inStream.ToArray());
						}
						catch(Exception e)
						{
							throw e;
						}
					}
				}
			}
			return result;
		}

		public static object ToObject(Type type, string value)
		{
			object o = null;
			if(type != null && value != null)
			{
				if(type == typeof(string))
				{
					return value;
				}
				else if(type == typeof(byte[]))
				{
					o = String2ByteArray(value);
				}
				else
				{
					TypeConverter  convert = TypeDescriptor.GetConverter(type) ;
					if(convert != null && convert.CanConvertFrom(typeof(string))
						&& convert.CanConvertTo(typeof(string)))
					{
						o = convert.ConvertFromString(value);
					}
					else 
					{
						byte[] buffer = String2ByteArray(value);
						IFormatter formatter = new BinaryFormatter();
						try
						{
							o = formatter.Deserialize(new MemoryStream(buffer));
						}
						catch(Exception e)
						{
							throw e;
						}
					}
				}

			}
			return o;
		}

		private static string ByteArray2String(byte[] byteArray)
		{
			string result = null;
			if(!IsEmpty(byteArray))
			{
				result = Convert.ToBase64String(byteArray);
			}
			return result;
		}

		private static byte[] String2ByteArray(string value)
		{
			byte[] byteArray = null;
			if(!IsEmpty(value))
			{
				try
				{
					byteArray = Convert.FromBase64String(value);
				}
				catch
				{
				}
			}
			return byteArray;
		}
		#endregion

		#region CreateObject
		public static object CreateObject(Type type, params object[] args)
		{
			if(type != null)
			{
				return Activator.CreateInstance(type, args);
			}
			return null;
		}

		public static object CreateObject(Type type)
		{
			return CreateObject(type, null);
		}

		public static object CreateObject(string typeName, params object[] args)
		{
			object o = null;
			Type type = GetType(typeName);
			if(type != null)
			{
				o = Activator.CreateInstance(type, args);
			}
			return o;
		}

		public static object CreateObject(string typeName)
		{
			return CreateObject(typeName, null);
		}
		#endregion

		#region Serialize
		public static void XmlSerializerObject(object value, Stream inStream)
		{
			if(value != null)
			{
				XmlSerializer sz = new XmlSerializer(value.GetType());
				sz.Serialize(inStream, value);
			}
		}

		public static string XmlSerializerObject(object value)
		{
			string result = null;
			if(value != null)
			{
				XmlSerializer sz = new XmlSerializer(value.GetType());
				StringBuilder sb = new StringBuilder();
				StringWriter writer = new StringWriter(sb);
				sz.Serialize(writer, value);
				result = sb.ToString();
			}
			return result;
		}

		public static object XmlDeserializeObject(Type type, Stream inStream)
		{
			object result = null;
			if(inStream != null)
			{
				XmlSerializer sz = new XmlSerializer(type);
				result = sz.Deserialize(inStream);
			}
			return result;
		}

		public static object XmlDeserializeObject(Type type, string value)
		{
			object result = null;
			if(!IsEmpty(value))
			{
				XmlSerializer sz = new XmlSerializer(type);
				StringReader reader = new StringReader(value);
				result = sz.Deserialize(reader);
			}
			return result;
		}
		#endregion

	    public static bool IsEmpty(string text)
	    {
            return string.IsNullOrEmpty(text);
	    }
	    
	    public static bool IsEmpty(ICollection collections)
	    {
            return collections == null || collections.Count == 0;
	    }
		#endregion
	}
}
