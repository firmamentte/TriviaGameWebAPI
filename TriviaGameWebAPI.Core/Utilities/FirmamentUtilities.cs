/*
using MyGeneration/Template/NHibernate (c) by Firmament
*/
using System.Net.Mail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace TriviaGameWebAPI.Core
{
    public static class FirmamentUtilities
    {

        #region PropertyDescriptor

        /// <summary>
        /// NB: Make sure you update MyGen!!!.
        /// Provides a description for an enumerated type.
        /// </summary>
        [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field,
         AllowMultiple = false)]
        public sealed class EnumDescriptionAttribute : Attribute
        {
            private readonly string description;

            /// <summary>
            /// Gets the description stored in this attribute.
            /// </summary>
            /// <value>The description stored in the attribute.</value>
            public string Description
            {
                get
                {
                    return this.description;
                }
            }

            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="EnumDescriptionAttribute"/> class.
            /// </summary>
            /// <param name="description">The description to store in this attribute.
            /// </param>
            public EnumDescriptionAttribute(string description)
                : base()
            {
                this.description = description;
            }
        }


        public static class EnumHelper
        {
            /// <summary>
            /// Gets the <see cref="DescriptionAttribute" /> of an <see cref="Enum" />
            /// type value.
            /// </summary>
            /// <param name="value">The <see cref="Enum" /> type value.</param>
            /// <returns>A string containing the text of the
            /// <see cref="DescriptionAttribute"/>.</returns>
        	public enum StandardEnum
        	{
            	[EnumDescription("Yes")]
            	Yes,
            	[EnumDescription("No")]
            	No,
            	[EnumDescription("Active")]
            	Active,
            	[EnumDescription("Inactive")]
            	Inactive,
            	[EnumDescription("Not Applicable")]
            	NotApplicable,
            	[EnumDescription("Don't Know")]
            	DontKnow,

        	}

			public enum SpecialApplicationUser
        	{
            	[EnumDescription("Default")]
            	Default,
            	[EnumDescription("Administrator")]
            	Administrator,
            	[EnumDescription("SuperUser")]
            	SuperUser,
        	}

            public static string GetDescription(Enum value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                string description = value.ToString();
                FieldInfo fieldInfo = value.GetType().GetField(description);
                EnumDescriptionAttribute[] attributes =
                   (EnumDescriptionAttribute[])
                 fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    description = attributes[0].Description;
                }
                return description;
            }

            /// <summary>
            /// Converts the <see cref="Enum" /> type to an <see cref="IList" /> 
            /// compatible object.
            /// </summary>
            /// <param name="type">The <see cref="Enum"/> type.</param>
            /// <returns>An <see cref="IList"/> containing the enumerated
            /// type value and description.</returns>
            public static IList ToList(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentNullException("type");
                }

                ArrayList list = new ArrayList();
                Array enumValues = Enum.GetValues(type);

                foreach (Enum value in enumValues)
                {
                    list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
                }

                return list;
            }
        }

        #endregion

        public static class EmailHelper
        {
        	public static bool IsEmailAddress(string emailAddress)
        	{
        		string strRegex =@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}"+
        					     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\"+
        					     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        		Regex re = new Regex(strRegex);
        		if (re.IsMatch(emailAddress))
        			return true;
        		else
        			return false;
        	}

        	public static void SendEmail(List<string> addrTo, string addrFrom, string addrFromPassword, string clientHost, int port, bool enableSsl, bool useDefaultCredentials, string subject, string mailBody, List<string> attachemts, List<LinkedResource> listLinkedResources)
        	{
        		try
        		{
        			MailMessage msg = new MailMessage();
        			foreach (string address in addrTo)
        			{
        				 msg.To.Add(address);
        			}
        			msg.From = new MailAddress(addrFrom);
        			msg.Subject = subject;
        			msg.Body = mailBody;
        			foreach (String item in attachemts)
        			{
        				 Attachment attachment = new Attachment(item);
        				 msg.Attachments.Add(attachment);
        			}
        			AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody, null, "text/html");
        			foreach (LinkedResource alinkedResource in listLinkedResources)
        			{
        				 htmlView.LinkedResources.Add(alinkedResource);
        			}
        			msg.AlternateViews.Add(htmlView);

        			SmtpClient client = null;

        			if (port <= 0)
        				client = new SmtpClient(clientHost);
        			else
        				client = new SmtpClient(clientHost, port);

        			if (enableSsl)
        				client.EnableSsl = true;

        			if (useDefaultCredentials)
        				client.UseDefaultCredentials = true;

        			client.Credentials = new System.Net.NetworkCredential(addrFrom, addrFromPassword);
        			client.Send(msg);
        		}
        		catch (SmtpException)
        		{
        			throw;
        		}
        		catch (Exception)
        		{
        			throw;
        		}
        	}

			public static void SendSMSViaEmail(string mailBody, string addrFrom, string addrFromPassword, string clientHost, int port, bool enableSsl, bool useDefaultCredentials)
			{
				try
				{
					MailMessage msg = new MailMessage();
					msg.To.Add("sms@messaging.clickatell.com");
					msg.From = new MailAddress(addrFrom);
					msg.Body = mailBody;
					msg.BodyEncoding = Encoding.UTF8;

					AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody, null, "text/html");
					msg.AlternateViews.Add(htmlView);

					SmtpClient client = null;

					if (port <= 0)
						client = new SmtpClient(clientHost);
					else
						client = new SmtpClient(clientHost, port);

					if (enableSsl)
						client.EnableSsl = true;

					if (useDefaultCredentials)
						client.UseDefaultCredentials = true;

					client.Credentials = new System.Net.NetworkCredential(addrFrom, addrFromPassword);
					client.Send(msg);
				}
				catch (SmtpException)
				{
					throw;
				}
				catch (Exception)
				{
					throw;
				}
			}
        }

         public static class RandomPasswordHelper
         {
        	// Define default min and max password lengths.
			private readonly static int DEFAULT_MIN_PASSWORD_LENGTH = 12;
			private readonly static int DEFAULT_MAX_PASSWORD_LENGTH = 13;
			// Define supported password characters divided into groups.
			// You can add (or remove) characters to (from) these groups.
			private readonly static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
			private readonly static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
			private readonly static string PASSWORD_CHARS_NUMERIC = "123456789";
			// The length of the generated password will be determined at
			// random. It will be no shorter than the minimum default and
			// no longer than maximum default.
			public static string Generate()
			{
				return Generate(DEFAULT_MIN_PASSWORD_LENGTH, DEFAULT_MAX_PASSWORD_LENGTH);
			}
			//Generates a random password of the exact length.
			public static string Generate(int length)
			{
				return Generate(length, length);
			}
			// The length of the generated password will be determined at
			// random and it will fall with the range determined by the
			// function parameters.
			public static string Generate(int minLength, int maxLength)
			{
				// Make sure that input parameters are valid.
				if (minLength < 0 || minLength == 0 || maxLength < 0 || maxLength == 0 || minLength > maxLength)
					return null;
				// Create a local array containing supported password characters
				// grouped by types. You can remove character groups from this
				// array, but doing so will weaken the password strength.
				char[][] charGroups = new char[][]
				{
					PASSWORD_CHARS_LCASE.ToCharArray(),
					PASSWORD_CHARS_UCASE.ToCharArray(),
					PASSWORD_CHARS_NUMERIC.ToCharArray()
					//PASSWORD_CHARS_SPECIAL.ToCharArray()
				};
				// Use this array to track the number of unused characters in each
				// character group.
				int[] charsLeftInGroup = new int[charGroups.Length];
				// Initially, all characters in each group are not used.
				for (int i = 0; i < charsLeftInGroup.Length; i++)
				charsLeftInGroup[i] = charGroups[i].Length;
				// Use this array to track (iterate through) unused character groups.
				int[] leftGroupsOrder = new int[charGroups.Length];
				// Initially, all character groups are not used.
				for (int i = 0; i < leftGroupsOrder.Length; i++)
				leftGroupsOrder[i] = i;
				// Because we cannot use the default randomizer, which is based on the
				// current time (it will produce the same "random" number within a
				// second), we will use a random number generator to seed the
				// randomizer.
				// Use a 4-byte array to fill it with random bytes and convert it then
				// to an integer value.
				byte[] randomBytes = new byte[4];
				// Generate 4 random bytes.
				RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
				rng.GetBytes(randomBytes);
				// Convert 4 bytes into a 32-bit integer value.
				int seed = (randomBytes[0] & 0x7f) << 24 | randomBytes[1] << 16 | randomBytes[2] << 8 | randomBytes[3];
				// Now, this is real randomization.
				Random random = new Random(seed);
				// This array will hold password characters.
				char[] password = null;
				// Allocate appropriate memory for the password.
				if (minLength < maxLength)
					password = new char[random.Next(minLength, maxLength + 1)];
				else
					password = new char[minLength];
				// Index of the next character to be added to password.
				int nextCharIdx;
				// Index of the next character group to be processed.
				int nextGroupIdx;
				// Index which will be used to track not processed character groups.
				int nextLeftGroupsOrderIdx;
				// Index of the last non-processed character in a group.
				int lastCharIdx;
				// Index of the last non-processed group.
				int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
				// Generate password characters one at a time.
				for (int i = 0; i < password.Length; i++)
				{
					// If only one character group remained unprocessed, process it;
					// otherwise, pick a random character group from the unprocessed
					// group list. To allow a special character to appear in the
					// first position, increment the second parameter of the Next
					// function call by one, i.e. lastLeftGroupsOrderIdx + 1.
					if (lastLeftGroupsOrderIdx == 0)
						nextLeftGroupsOrderIdx = 0;
					else
						nextLeftGroupsOrderIdx = random.Next(0, lastLeftGroupsOrderIdx);
					// Get the actual index of the character group, from which we will pick the next character.
					nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];
					// Get the index of the last unprocessed characters in this group.
					lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;
					// If only one unprocessed character is left, pick it; otherwise,
					// get a random character from the unused character list.
					if (lastCharIdx == 0)
						nextCharIdx = 0;
					else
						nextCharIdx = random.Next(0, lastCharIdx + 1);
					// Add this character to the password.
					password[i] = charGroups[nextGroupIdx][nextCharIdx];
					// If we processed the last character in this group, start over.
					 if (lastCharIdx == 0)
						charsLeftInGroup[nextGroupIdx] = charGroups[nextGroupIdx].Length;
					 // There are more unprocessed characters left.
					else
					{
						// Swap processed character with the last unprocessed character
						// so that we don't pick it until we process all characters in this group.
						if (lastCharIdx != nextCharIdx)
						{
							char temp = charGroups[nextGroupIdx][lastCharIdx];
							charGroups[nextGroupIdx][lastCharIdx] = charGroups[nextGroupIdx][nextCharIdx];
							charGroups[nextGroupIdx][nextCharIdx] = temp;
						}
						// Decrement the number of unprocessed characters in this group.
						charsLeftInGroup[nextGroupIdx]--;
					}
					// If we processed the last group, start all over.
					if (lastLeftGroupsOrderIdx == 0)
						lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
						// There are more unprocessed groups left.
					else
						{
							// Swap processed group with the last unprocessed group
							// so that we don't pick it until we process all groups.
							if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
							{
								int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
								leftGroupsOrder[lastLeftGroupsOrderIdx] = leftGroupsOrder[nextLeftGroupsOrderIdx];
								leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
							}
							// Decrement the number of unprocessed groups.
							lastLeftGroupsOrderIdx--;
						}
				}
				// Convert password characters into a string and return the result.
				 return new string(password);
			}
         }

       	public static class ApplicationPathHelper
        {
        	public static string ApplicationPath(HttpContext context)
        	{
        			var appPath = string.Empty;

        			if (context != null)
        			{
        				appPath = string.Format("{0}://{1}{2}{3}",
        										context.Request.Scheme,
        										context.Request.Host.Host,
        										(context.Request.Host.Port == 80 && context.Request.Scheme == "http") || (context.Request.Host.Port == 443 && context.Request.Scheme == "https")
        									     ? string.Empty
        									     : ":" + context.Request.Host.Port,
        									     context.Request.PathBase);
        			}

					if (!appPath.EndsWith("/"))
						appPath += "/";

					return appPath;
        	}
        }

        public static  class DatabaseHelper
        {
            public static string ConnectionString { get; set; }

            public static string DatabaseDataSource { get; set; }

            public static string DatabaseInitialCatalog { get; set; }

            public static string DatabaseUsername { get; set; }

            public static string DatabasePassword { get; set; }

            public static  SqlDataReader ExecuteSqlDataReader(string storedProcedureName, ArrayList parameters)
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
				}
                SqlCommand comm = new SqlCommand(storedProcedureName, connection);
                foreach (SqlParameter param in parameters)
                {
                    comm.Parameters.Add(param);
				}
   				comm.CommandType = CommandType.StoredProcedure;
				SqlDataReader dr = comm.ExecuteReader();
				return dr;
			}

            public static  int ExecuteNonQuery(string storedProcedureName, ArrayList parameters)
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
				}
                SqlCommand comm = new SqlCommand(storedProcedureName, connection);
                foreach (SqlParameter param in parameters)
                {
                    comm.Parameters.Add(param);
				}
   				comm.CommandType = CommandType.StoredProcedure;
				return comm.ExecuteNonQuery();
			}

            public static  DataTable ExecuteQuery(string storedProcedureName, ArrayList parameters)
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(storedProcedureName, connection) };
                foreach (SqlParameter param in parameters)
                {
                    adapter.SelectCommand.Parameters.Add(param);
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                adapter.Fill(ds, "tableData");
                return ds.Tables["tableData"];
            }

            public static  object ExecuteScaler(string storedProcedureName, ArrayList parameters)
            {
                SqlConnection connection = new SqlConnection(ConnectionString);

                SqlCommand command = new SqlCommand(storedProcedureName, connection) { CommandType = CommandType.StoredProcedure };
                foreach (SqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
                object result = null;
                try
                {
                    connection.Open();
                    result = command.ExecuteScalar();

                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                    command.Dispose();
                }
                return result;
            }

            static public SqlParameter CreateIntParameter(string name, int value)
            {
                return new SqlParameter(name, SqlDbType.Int) { Value = value };
            }
            static public SqlParameter CreateDecimalParameter(string name, decimal value)
            {
                return new SqlParameter(name, SqlDbType.Decimal) { Value = value };
            }
            static public SqlParameter CreateStringParameter(string name, string value)
            {
                return new SqlParameter(name, SqlDbType.NVarChar) { Value = value };
            }
            static public SqlParameter CreateDateTimeParameter(string name, DateTime value)
            {
                return new SqlParameter(name, SqlDbType.DateTime) { Value = value };
            }
            static public SqlParameter CreateUniqueIdentifierParameter(string name, Guid value)
            {
                return new SqlParameter(name, SqlDbType.UniqueIdentifier) { Value = value };
            }
        }

        public static class CommsHelper
        { }

        public static class WorkflowHelper
        { }

        public static class ReportHelper
        { }

        public static class NumberHelper
        {
			public static string GetNumberAbbreviation(decimal number)
			{
				try
				{
					string result = string.Empty;

					if (number > 9999)
					{
						if (number >= 1000000)
						{
							if (number >= 1000000000)
							{
								result = Math.Round(number / 1000000000M, 1).ToString() + "B";
							}
							else
							{
								result = Math.Round(number / 1000000M, 1).ToString() + "M";
							}
						}
						else
						{
							result = Math.Round(number / 1000M, 1).ToString() + "K";
						}
					}
					else
					{
						result = Math.Round(number).ToString("N0");
					}
					return result;
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

        public static class ValidationHelper
        {
			public static bool IsStringContainsStroke(string stringValue)
			{
				bool result = false;

				if (!string.IsNullOrWhiteSpace(stringValue))
				{
					if (stringValue.Contains("|"))
						result = true;
				}

				return result;
			}

            // Function to test for Positive Integers. 
            public static bool IsNaturalNumber(String strNumber)
            {
                Regex objNotNaturalPattern = new Regex("[^0-9]");
                Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
                return !objNotNaturalPattern.IsMatch(strNumber) &&
                objNaturalPattern.IsMatch(strNumber);
            }
            // Function to test for Positive Integers with zero inclusive 
            public static bool IsWholeNumber(String strNumber)
            {
                Regex objNotWholePattern = new Regex("[^0-9]");
                return !objNotWholePattern.IsMatch(strNumber);
            }
            // Function to Test for Integers both Positive and Negative 
            public static bool IsInteger(String strNumber)
            {
                Regex objNotIntPattern = new Regex("[^0-9-]");
                Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
                return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
            }
                      public static bool IsHumanName(String strToCheck) 
                        {
                            Regex objAlphaPattern = new Regex("[^ a-zA-Z'-]");
                            return !objAlphaPattern.IsMatch(strToCheck);
                        }
            // Function to Test for Positive Number both Integer and Real 
            public static bool IsPositiveNumber(String strNumber)
            {
                Regex objNotPositivePattern = new Regex("[^0-9.]");
                Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
                Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
                return !objNotPositivePattern.IsMatch(strNumber) &&
                objPositivePattern.IsMatch(strNumber) &&
                !objTwoDotPattern.IsMatch(strNumber);
            }
            // Function to test whether the string is valid number or not
            public static bool IsNumber(String strNumber)
            {
                Regex objNotNumberPattern = new Regex("[^0-9.-]");
                Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
                Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
                String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
                String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
                Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
                return !objNotNumberPattern.IsMatch(strNumber) &&
                !objTwoDotPattern.IsMatch(strNumber) &&
                !objTwoMinusPattern.IsMatch(strNumber) &&
                objNumberPattern.IsMatch(strNumber);
            }
            // Function To test for Alphabets. 
            public static bool IsAlpha(String strToCheck)
            {
		     Regex objAlphaPattern = new Regex("[^a-zA-Z]");
                return !objAlphaPattern.IsMatch(strToCheck);
            }
            // Function to Check for AlphaNumeric.
            public static bool IsAlphaNumeric(String strToCheck)
            {
                Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
                return !objAlphaNumericPattern.IsMatch(strToCheck);
            }

			private static bool ValidateSaId(string IDNumber)
			{
				string varOdd = string.Empty;
				int varEven = 0;
				int varOddSum = 0;
				Int32 VarSum = 0;
				string varLastDigit = string.Empty;

				try
				{
					if (IDNumber.Length == 13)
					{
						string varCheckDigit = varLastDigit = IDNumber.Substring(12, 1);
						for (int ix = 0; ix < IDNumber.Length - 1; ix++)
						{
							if (ix % 2 > 0)
							{
								varOdd += Convert.ToInt16(IDNumber.Substring(ix, 1));
							}
							else
							{
								varEven += Convert.ToInt16(IDNumber.Substring(ix, 1));
							}
						}
						varOdd = (Convert.ToInt32(varOdd) * 2).ToString();
						foreach (char c in varOdd.ToCharArray())
							varOddSum += Convert.ToInt16(c.ToString());
						VarSum = Convert.ToInt32(varEven) + Convert.ToInt32(varOddSum);
						varLastDigit = VarSum.ToString().Substring(VarSum.ToString().Length - 1, 1);
						varLastDigit = (10 - Convert.ToInt16(varLastDigit)).ToString();
						if (varLastDigit.Length == 2)
						{
							if (varLastDigit.Substring(1, 1) == varCheckDigit.ToString())
							{
								return true;
							}
						}
						else if (varLastDigit.ToString() == varCheckDigit.ToString())
						{
							return true;
						}
					}
					return false;
				}
				catch (Exception)
				{
					return false;
				}
			}

			public static bool IsSaIdValid(string SAID)
			{
				return ValidateSaId(SAID) != false;
			}

			public static bool IsGenderValid(string SAID, string genderName)
			{
				string _genderName = string.Empty;
				if (Convert.ToInt32(SAID.Substring(6, 4)) < 5000)
					_genderName = "Female";
				else
					_genderName = "Male";
				if (genderName == _genderName)
					return true;
				else
					return false;
			}

			public static bool IsDateOfBirthValid(string SAID, DateTime dateOfBirth)
			{
				int day = Convert.ToInt32(SAID.Substring(4, 2)), month = Convert.ToInt32(SAID.Substring(2, 2)), year = Convert.ToInt32(SAID.Substring(0, 2));
				if ((dateOfBirth.Day == day) && (dateOfBirth.Month == month) && (Convert.ToInt32(dateOfBirth.Year.ToString().Substring(2, 2)) == year))
					return true;
				else
					return false;
			}
        }

		public partial class DefaultValueHelper
        {
            public static string DefaultValue
            {
                get
                {
                    return "-999995";
                }
            }
            public static string MissingValue
            {
                get
                {
                    return  "-999999";
                }
            }
            public static string NotApplicableValue
            {
                get
                {
                    return  "-999997";
                }
            }

        }

        public static class DateHelper
        {
			public static DateTime GetFromDate(int year, int month, int day)
			{
				try
				{
					int _year, _month, _day;

					if (year != 0)
					{
						_year = year;

						if (month != 0)
						{
							_month = month;
							if (day != 0)
								_day = day;
							else
								_day = 1;
						}
						else
						{
							 _month = 1;
							 _day = 1;
						}
					}
					else
					{
						_year = DefaultDate.Year;
						_month = DefaultDate.Month;
						_day = DefaultDate.Day;
					}

					return new DateTime(_year, _month, _day);
				}
				catch (Exception)
				{
					throw;
				}
			}

			public static DateTime GetToDate(int year, int month, int day)
			{
				try
				{
					int _year, _month, _day;

					if (year != 0)
					{
						_year = year;

						if (month != 0)
						{
							_month = month;

							if (day != 0)
								_day = day;
							else
								_day = FirmamentUtilities.DateHelper.GetLastDayOfMonth(new DateTime(_year, _month, 1)).Day;
						}
						else
						{
							_month = 12;
							_day = FirmamentUtilities.DateHelper.GetLastDayOfMonth(new DateTime(_year, _month, 1)).Day;
						}
					}
					else
					{
						_year = DefaultDate.Year;
						_month = DefaultDate.Month;
						_day = DefaultDate.Day;
					}

					return new DateTime(_year, _month, _day);
				}
				catch (Exception)
				{
					throw;
				}
			}

            public static DateTime DefaultDate
            {
                get
                {
                   return Convert.ToDateTime("9995-01-01");
                }
            }

            public static DateTime NotApplicableDate
            {
                get
                {
                    return Convert.ToDateTime("9997-01-01");
                }
            }

            public static DateTime MissingDate
            {
                get
                {
                    return Convert.ToDateTime("9999-01-01");
                }
            }
	            public static DateTime EarliestDate 
            {
                get
                {
                    return DateTime.MinValue;
                }
            }
            /// <summary>
            /// Get the first day of the month for
            /// any full date submitted
            /// </summary>
            /// <param name="dtDate"></param>
            /// <returns></returns>
            public static DateTime GetFirstDayOfMonth(DateTime dtDate)
            {
                // set return value to the first day of the month
                // for any date passed in to the method
                // create a datetime variable set to the passed in date
                DateTime dtFrom = dtDate;
                // remove all of the days in the month
                // except the first day and set the
                // variable to hold that date
                dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));
                // return the first day of the month
                return dtFrom.Date;
            }


            /// <summary>

            /// Get the first day of the month for a

            /// month passed by it's integer value

            /// </summary>

            /// <param name="iMonth"></param>

            /// <returns></returns>

            public static DateTime GetFirstDayOfMonth(int iMonth)
            {

                // set return value to the last day of the month

                // for any date passed in to the method



               // create a datetime variable set to the passed in date

                DateTime dtFrom = new DateTime(DateTime.Now.Year, iMonth, 1);



                // remove all of the days in the month
                // except the first day and set the
                // variable to hold that date
                dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));
                // return the first day of the month
                return dtFrom.Date;
            }
            /// <summary>
            /// Get the last day of the month for any
            /// full date
            /// </summary>
            /// <param name="dtDate"></param>
            /// <returns></returns>
            public static DateTime GetLastDayOfMonth(DateTime dtDate)
            {
                // set return value to the last day of the month
                // for any date passed in to the method
                // create a datetime variable set to the passed in date
                DateTime dtTo = dtDate;
                // overshoot the date by a month
                dtTo = dtTo.AddMonths(1);

                // remove all of the days in the next month
                // to get bumped down to the last day of the 
                // previous month
                dtTo = dtTo.AddDays(-(dtTo.Day));
                // return the last day of the month
                return dtTo.Date.AddDays(1).AddMilliseconds(-1);
            }
            /// <summary>
            /// Get the last day of a month expressed by it's
            /// integer value
           /// </summary>
            /// <param name="iMonth"></param>
           /// <returns></returns>
           public static DateTime GetLastDayOfMonth(int iMonth)
            {
                // set return value to the last day of the month
                // for any date passed in to the method
                // create a datetime variable set to the passed in date
                DateTime dtTo = new DateTime(DateTime.Now.Year, iMonth, 1);
                // overshoot the date by a month
                dtTo = dtTo.AddMonths(1);
                // remove all of the days in the next month
                // to get bumped down to the last day of the 
                // previous month
                dtTo = dtTo.AddDays(-(dtTo.Day));
                // return the last day of the month
                return dtTo;
            }

            public class StopWatch
            {

                private DateTime startTime;
                private DateTime stopTime;
                private bool running = false;
                public void Start()
                {
                    this.startTime = DateTime.Now;
                    this.running = true;
                }
                public void Stop()
                {
                    this.stopTime = DateTime.Now;
                    this.running = false;
                }
                // elaspsed time in milliseconds
                public double GetElapsedTime()
                {
                    TimeSpan interval;
                    if (running)
                        interval = DateTime.Now - startTime;
                    else
                        interval = stopTime - startTime;
                    return interval.TotalMilliseconds;
                }
                // elaspsed time in seconds
               public double GetElapsedTimeSecs()
                {
                    TimeSpan interval;

                    if (running)
                        interval = DateTime.Now - startTime;
                    else
                        interval = stopTime - startTime;

                    return interval.TotalSeconds;
                }
            }
        }
	}
}

