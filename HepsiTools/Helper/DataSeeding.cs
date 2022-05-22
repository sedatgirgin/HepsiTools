using HepsiTools.DataAccess;
using HepsiTools.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HepsiTools.Helper
{
    public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ToolDbContext>();

            if (context.CompetitionCompany.ToList().Count==0)
            {
                List <CompetitionCompany> CompetitionCompanyList =  new List<CompetitionCompany>();

                foreach (var value in Enum.GetValues(typeof(CompanyType)))
                    CompetitionCompanyList.Add(new CompetitionCompany() { CompanyType = (CompanyType)value });

                context.CompetitionCompany.AddRange(CompetitionCompanyList);
            }

            if (context.Lisans.ToList().Count == 0)
            {
                context.Lisans.AddRange(new List<Lisans>() {  
                    new Lisans() { Name="WooCommerce"}, 
                    new Lisans() { Name="MultiWooCommerce"},
                    new Lisans() { Name="CompetitionAnalyses"}
                });
            }



            context.SaveChanges();
        }
    }
}
