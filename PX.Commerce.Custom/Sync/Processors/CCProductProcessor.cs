using PX.Api.ContractBased.Models;
using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Objects;
using PX.Commerce.Custom.API.REST;
using PX.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace PX.Commerce.Custom
{
    public abstract class CCProductProcessor<TGraph, TEntityBucket, TPrimaryMapped> : BCProcessorSingleBase<TGraph, TEntityBucket, TPrimaryMapped>, IProcessor
        where TGraph : PXGraph
        where TEntityBucket : class, IEntityBucket, new()
        where TPrimaryMapped : class, IMappedEntity, new()
    {

        protected ProductRestDataProvider productDataProvider;

        protected IChildRestDataProvider<ProductImageData> productImageDataProvider;

        protected Dictionary<int, string> SalesCategories;

        protected ProductCategoryRestDataProvider categoryDataProvider;
        protected UploadFileMaintenance uploadGraph;

        public CommerceHelper helper = PXGraph.CreateInstance<CommerceHelper>();

        public override void Initialise(IConnector connector, ConnectorOperation operation)
        {
            base.Initialise(connector, operation);

            var client = CCConnector.GetRestClient(GetBindingExt<BCBindingCustom>());

            productDataProvider = new ProductRestDataProvider(client);

            productImageDataProvider = new ProductImageRestDataProvider(client);

            categoryDataProvider = new ProductCategoryRestDataProvider(client);

            SalesCategories = new Dictionary<int, string>();

            uploadGraph = PXGraph.CreateInstance<UploadFileMaintenance>();
            helper.Initialize(this);
        }

        public virtual ProductImageData GetImageFromUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var task = Task.Run(() => client.GetAsync(url));
                task.Wait();
                var response = task.Result;
                response.EnsureSuccessStatusCode();

                ProductImageData image = new ProductImageData();

                var contentTask = Task.Run(() => response.Content.ReadAsByteArrayAsync());
                contentTask.Wait();
                var bytes = contentTask.Result;

                image.FileContent = Convert.ToBase64String(bytes);
                image.FileMimeType = response.Content.Headers.ContentType?.MediaType;
                image.FileName = response.Content.Headers.ContentDisposition?.FileName;

                return image;
            }
        }

        public static string GetMimeTypeFromImageByteArray(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
            {
                return System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().First(codec => codec.FormatID == image.RawFormat.Guid).MimeType;
            }
        }

        readonly static Uri SomeBaseUri = new Uri("http://canbeanything");

        static string GetFileNameFromUrl(string url)
        {
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                uri = new Uri(SomeBaseUri, url);

            return Path.GetFileName(uri.LocalPath);
        }

        public virtual void SaveMainImage(IMappedEntity obj, string itemCD, List<string> fileList)
        {
            if (fileList.Count() == 0) return;

            List<ProductImageData> imageList = productImageDataProvider.GetAll(obj.ExternID).ToList();

            foreach (string fileID in fileList)
            {
                try
                {
                    SM.FileInfo file = uploadGraph.GetFile(fileID);
                    ProductImageData productImageData = null;
                    string fileName = itemCD + "_" + HttpUtility.UrlPathEncode(Path.GetFileNameWithoutExtension(file.Name));


                    if (imageList?.Count() > 0)
                    {
                        productImageData = imageList.FirstOrDefault(x => x.Url.Contains(fileName));

                        if (productImageData != null)
                        {
                            if (obj.Details?.Any(x => x.EntityType == BCEntitiesAttribute.ProductImage && x.LocalID == file.UID) == false)
                            {
                                obj.AddDetail(BCEntitiesAttribute.ProductImage, file.UID.Value, productImageData.Id);
                            }
                            continue;
                        }
                    }

                    //Creating the image
                    productImageData = new ProductImageData()
                    {
                        Position = string.Equals("main", file.Comment, StringComparison.OrdinalIgnoreCase) || fileList.Count() == 1 ? "0" : null,
                        FileContent = Convert.ToBase64String(file.BinData),
                        FileMimeType = GetMimeTypeFromImageByteArray(file.BinData),
                        FileName = fileName,
                    };

                    productImageData = productImageDataProvider.Create(productImageData, obj.ExternID);
                    if (obj.Details?.Any(x => x.EntityType == BCEntitiesAttribute.ProductImage && x.LocalID == file.UID) == false)
                    {
                        obj.AddDetail(BCEntitiesAttribute.ProductImage, file.UID.Value, productImageData.Id);
                    }

                }
                catch (Exception e)
                {
                    throw;
                }
            }  
        }

        public virtual void SaveEcommerceUrlImages(IMappedEntity obj, List<InventoryFileUrls> urls)
        {
            var fileUrls = urls?.Where(x => x.FileType?.Value == BCCaptions.Image && !string.IsNullOrEmpty(x.FileURL?.Value))?.ToList();
            if (fileUrls == null || fileUrls.Count() == 0) return;

            List<ProductImageData> imageList = null;
            foreach(var image in fileUrls)
            {
                ProductImageData productImageData = null;

                try
                {
                    if(imageList ==  null)
                    {
                        imageList = productImageDataProvider.GetAll(obj.ExternID).ToList();
                    }

                    if(imageList?.Count() > 0)
                    {
                        productImageData = imageList.FirstOrDefault(x => string.Equals(x.Url, image.FileURL.Value, StringComparison.OrdinalIgnoreCase));

                        if(productImageData!= null)
                        {
                            if(obj.Details?.Any(x => x.EntityType == BCEntitiesAttribute.ProductImage && x.LocalID == image.NoteID?.Value) == false)
                            {
                                obj.AddDetail(BCEntitiesAttribute.ProductImage, image.NoteID.Value, productImageData.Id);
                            }
                            continue;
                        }
                    };

                    //Creating the image
                    productImageData = GetImageFromUrl(image.FileURL.ToString());
                    //new ProductImageData()
                    //{
                    //    Url = Uri.EscapeUriString(System.Web.HttpUtility.UrlDecode(image.FileURL.Value)),
                    //};
                    productImageData = productImageDataProvider.Create(productImageData, obj.ExternID);
                    if (obj.Details?.Any(x => x.EntityType == BCEntitiesAttribute.ProductImage && x.LocalID == image.NoteID?.Value) == false)
                    {
                        obj.AddDetail(BCEntitiesAttribute.ProductImage, image.NoteID.Value, productImageData.Id);
                    }
                    imageList = imageList ?? new List<ProductImageData>();
                    imageList.Add(productImageData);

                    productImageData = new ProductImageData();

                } catch (Exception e)
                {
                    throw;
                }
            }
        }

        public virtual void SetProductStatus(ProductData data, string status, string availability, string visibility)
        {
            if (availability != BCCaptions.DoNotUpdate)
            {
                if (status.Equals(PX.Objects.IN.Messages.Inactive) || status.Equals(PX.Objects.IN.Messages.NoSales) || status.Equals(PX.Objects.IN.Messages.ToDelete))
                {
                    data.Active = Status.Inactive;
                    data.Visibility = Visibility.Hidden;
                }
                else
                {
                    data.Active = Status.Active;
                    if (visibility == BCCaptions.Invisible || availability == BCCaptions.Disabled)
                    {
                        data.Visibility = Visibility.Hidden;
                    }
                    else if(visibility == BCCaptions.Featured)
                    {
                        data.Visibility = Visibility.Search;
                    } else
                    {
                        data.Visibility = Visibility.Visible;
                    }
                }
            }
        }

    }
}
