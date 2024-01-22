using StorageApi.Abstract;
using StorageApi.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StorageApi.Mutation
{
    public class MySimpleMutation
    {
        public int AddStorage([Service] IStorageService service, StorageDto storage)
        {
            int id = service.AddStorage(storage);
            return id;
        }
        public StorageDto AddProductToStorage([Service] IStorageService service, int storageId, int productId, int count)
        {
            StorageDto result = service.AddProductToStorage(storageId, productId, count);
            return result;
        }

    }
}


//mutation addStorage
//{
//    addStorage(storage: {
//    id: 0, name: "first", description: "none", count: 5, products: []
//  })
//}

//query getProduct
//{
//    product(productId: 4){ id, name}
//}