using ProniaBackEnd.Database.Models;
using ProniaBackEnd.ViewModels.admin.products;

namespace ProniaBackEnd.Mapper
{
    public static class UpdateMapper<TModel,TViewModel>
    {

        public static ProductUpdateViewModel Handle(object Model)
        {
            Type productType = typeof(TModel);
            Type ProductUpdateVMType = typeof(TViewModel);

            ProductUpdateViewModel productUpdateViewModel = new ProductUpdateViewModel();

            foreach (var item in productType.GetProperties())
            {
                var propInfo = ProductUpdateVMType.GetProperty(item.Name);

                if (propInfo != null && propInfo.CanWrite)
                {
                    propInfo.SetValue(productUpdateViewModel, item.GetValue(Model));
                }
            }

            return productUpdateViewModel;  
        }
    }
}
