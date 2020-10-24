// -----------------------------------------------------------------------
// <copyright file="ExcelSeederBase.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Devkit.Data.Interfaces;
    using OfficeOpenXml;
    using OfficeOpenXml.Table;

    /// <summary>
    /// The base class for importing Excel seed file into the database.
    /// </summary>
    /// <seealso cref="SeederBase{ExcelWorksheets}" />
    public abstract class ExcelSeederBase : SeederBase<ExcelWorksheets>
    {
        /// <summary>
        /// The file information.
        /// </summary>
        private readonly FileInfo _fileInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelSeederBase" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="seederConfig">The seeder configuration.</param>
        protected ExcelSeederBase(IRepository repository, ISeederConfig seederConfig)
            : base(repository)
        {
            if (!string.IsNullOrEmpty(seederConfig?.FilePath) && File.Exists(seederConfig.FilePath))
            {
                this._fileInfo = new FileInfo(seederConfig.FilePath);
                this.InitializeSource();
            }
        }

        /// <summary>
        /// Imports the excel to list.
        /// </summary>
        /// <typeparam name="T">The underlying type of the list.</typeparam>
        /// <param name="tabName">Name of the tab.</param>
        /// <returns>
        /// A list of <typeparamref name="T" />.
        /// </returns>
        /// <exception cref="NotImplementedException">If a type does not have a conversion implementation.</exception>
        public List<T> DeserializeTabToList<T>(string tabName)
            where T : new()
        {
            //DateTime Conversion
            var convertDateTime = new Func<double, DateTime>(excelDate =>
            {
                if (excelDate < 1)
                {
                    throw new ArgumentException("Excel dates cannot be smaller than 0.");
                }

                DateTime dateOfReference = new DateTime(1900, 1, 1);

                if (excelDate > 60d)
                {
                    excelDate = excelDate - 2;
                }
                else
                {
                    excelDate = excelDate - 1;
                }

                return dateOfReference.AddDays(excelDate);
            });

            ExcelTable table = null;

            var worksheet = this.Source[tabName];

            if (worksheet.Tables.Any())
            {
                table = worksheet.Tables.FirstOrDefault();
            }
            else
            {
                table = worksheet.Tables.Add(worksheet.Dimension, "tbl" + Guid.NewGuid().ToString());

                var newAddress = new ExcelAddressBase(table.Address.Start.Row, table.Address.Start.Column, table.Address.End.Row + 1, table.Address.End.Column);

                //Edit the raw XML by searching for all references to the old address
                table.TableXml.InnerXml = table.TableXml.InnerXml.Replace(table.Address.ToString(), newAddress.ToString());
            }

            //Get the cells based on the table address
            var groups = table.WorkSheet.Cells[table.Address.Start.Row, table.Address.Start.Column, table.Address.End.Row, table.Address.End.Column]
                .GroupBy(cell => cell.Start.Row)
                .ToList();

            //Assume the second row represents column data types (big assumption!)
            var types = groups.Skip(1).FirstOrDefault().Select(rcell => rcell.Value.GetType()).ToList();

            //Get the properties of T
            var modelProperties = new T().GetType().GetProperties().ToList();

            //Assume first row has the column names
            var colnames = groups.FirstOrDefault()
                .Select((hcell, idx) => new
                {
                    Name = hcell.Value.ToString(),
                    index = idx
                })
                .Where(o => modelProperties.Select(p => p.Name).Contains(o.Name))
                .ToList();

            //Everything after the header is data
            var rowvalues = groups
                .Skip(1) //Exclude header
                .Select(cg => cg.Select(c => c.Value).ToList()).ToList();

            //Create the collection container
            var collection = new List<T>();

            foreach (List<object> row in rowvalues)
            {
                T tnew = new T();
                foreach (var colname in colnames)
                {
                    //This is the real wrinkle to using reflection - Excel stores all numbers as double including int
                    object val = row[colname.index];
                    var type = types[colname.index];
                    var prop = modelProperties.FirstOrDefault(p => p.Name == colname.Name);

                    //If it is numeric it is a double since that is how excel stores all numbers
                    if (type == typeof(double))
                    {
                        //Unbox it
                        var unboxedVal = (double)val;

                        //FAR FROM A COMPLETE LIST!!!
                        if (prop.PropertyType == typeof(int))
                        {
                            prop.SetValue(tnew, (int)unboxedVal);
                        }
                        else if (prop.PropertyType == typeof(double))
                        {
                            prop.SetValue(tnew, unboxedVal);
                        }
                        else if (prop.PropertyType == typeof(DateTime))
                        {
                            prop.SetValue(tnew, convertDateTime(unboxedVal));
                        }
                        else if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(tnew, val.ToString());
                        }
                        else
                        {
                            throw new NotImplementedException(string.Format("Type '{0}' not implemented yet!", prop.PropertyType.Name));
                        }
                    }
                    else
                    {
                        //Its a string
                        prop.SetValue(tnew, val);
                    }
                }
                collection.Add(tnew);
            }

            return collection;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void InitializeSource()
        {
            using (var package = new ExcelPackage(this._fileInfo))
            {
                this.Source = package.Workbook.Worksheets;
            }
        }
    }
}