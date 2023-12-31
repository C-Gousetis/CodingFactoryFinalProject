using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using RandomNameGenerator.Application.DTO;
using RandomNameGenerator.Domain;
using RandomNameGenerator.Infrastructure;

namespace RandomNameGenerator.Application.Services.Implementations
{
    public class Name : IName
    {
        private readonly DataContext _dataContext;

        public Name(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<List<NamesDTO>> GetNames(bool id)
        {
            var response = await _dataContext.Names.Where(x => x.GenderId == id).ToListAsync();
            if (!response.Any())
            {
                return await SeedData(id);
            }

            return response
                .OrderBy(x => Guid.NewGuid())
                .Select(x => new NamesDTO(x.Name, x.GenderId))
                .Take(10)
                .ToList();
        }

        private async Task<List<NamesDTO>> SeedData(bool id)
        {
            var names = GetNamesFromExcel();
            await _dataContext.Names.AddRangeAsync(names);
            await _dataContext.SaveChangesAsync();

            return names
                .OrderBy(x => Guid.NewGuid())
                .Select(x => new NamesDTO(x.Name, x.GenderId))
                .Where(x => x.GenderId == id)
                .Take(10)
                .ToList();
        }

        private List<Names> GetNamesFromExcel()
        {
            string file = @"C:\tmp\NamesData.xlsx";
            FileInfo existingFile = new FileInfo(file);
            List<Names> names = new List<Names>();
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                // Get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets["names"];
                int colCount = worksheet.Dimension.End.Column; // Get Column Count
                int rowCount = worksheet.Dimension.End.Row; // Get Row Count

                
                for (int row = 2; row <= rowCount; row++)
                {
                    var name = worksheet.Cells[row, 2].Value?.ToString().Trim();
                    var genderId = worksheet.Cells[row, 3].Value?.ToString().Trim();
                    names.Add(new Names {Name = name, GenderId = genderId == "0" ? false : true });
                }
            }
            return names;
        }
    }
}
