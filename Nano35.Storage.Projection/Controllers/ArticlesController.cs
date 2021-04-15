using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BarcodeLib;
using FluentValidation;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Projection.UseCases;
using Nano35.Storage.Projection.UseCases.PresentationGetAllArticles;
using Font = iTextSharp.text.Font;
using Image = System.Drawing.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace Nano35.Storage.Projection.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        
        public ArticlesController(IServiceProvider services) { _services = services; }
        
        [HttpGet]
        [Route("GetAllArticles")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PresentationGetAllArticlesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PresentationGetAllArticlesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticles(
            [FromQuery] PresentationGetAllArticlesHttpQuery query)
        {
            return await new CanonicalizedPresentationGetAllArticlesRequestRequest(
                new LoggedPipeNode<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract>(
                    _services.GetService(typeof(ILogger<IPresentationGetAllArticlesRequestContract>)) as ILogger<IPresentationGetAllArticlesRequestContract>,
                    new ValidatedPipeNode<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract>(
                        _services.GetService(typeof(IValidator<IPresentationGetAllArticlesRequestContract>)) as IValidator<IPresentationGetAllArticlesRequestContract>,
                        new PresentationGetAllArticlesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        }
    }
}


//public static void GetBarcode(int height, int width, BarcodeLib.TYPE type, string code, out System.Drawing.Image image, string fileSaveUrl)
//{
//    try
//    {
//        image = null;
//
//        var font = new System.Drawing.Font("verdana", 10f);
//                
//        var b = new BarcodeLib.Barcode
//        {
//            BackColor = System.Drawing.Color.White,
//            ForeColor = System.Drawing.Color.Black,
//            ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg,
//            Alignment = AlignmentPositions.CENTER,
//            LabelFont = font,
//            Height = height,
//            Width = width
//        };
//
//        image = b.Encode(type, code);
//        image.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
//
//    }
//    catch (Exception err)
//    {
//        err.ToString();
//        image = null;
//    }
//}

//GetBarcode(400, 500, TYPE.CODE128, storageItemId.ToString(), out var image, @$"{Directory.GetCurrentDirectory()}\{storageItemId.ToString()}.jpeg");
//var pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
//pic.Alignment = 1;
//            
//Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//            
//var baseFont = BaseFont.CreateFont(
//    @"C:\Windows\Fonts\arial.ttf",
//    BaseFont.IDENTITY_H,
//    BaseFont.NOT_EMBEDDED);
//            
//var font = new Font(
//    baseFont, 
//    Font.DEFAULTSIZE,
//    Font.NORMAL)
//{
//    Size = 10
//};
//            
//var document = new Document();
//            
//var path = @$"{Directory.GetCurrentDirectory()}\{Guid.NewGuid().ToString()}";
//var stream = new FileStream(path, FileMode.Create);
//            
//PdfWriter.GetInstance(document, stream);
//
//var rec = new Rectangle(PageSize.A6.Rotate())
//{
//    BackgroundColor = new BaseColor(255, 255, 255)
//};
//
//document.SetPageSize(rec); 
//document.SetMargins(0, 0, 0, 0);
//            
//document.Open();
//document.Add(pic);
//document.Close();
//            
//const string mimeType = "application/pdf";
//return new FileStreamResult(new FileStream(path, FileMode.Open), mimeType);

//Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//
//var baseFont = BaseFont.CreateFont(
//    @"C:\Windows\Fonts\arial.ttf",
//    BaseFont.IDENTITY_H,
//    BaseFont.NOT_EMBEDDED);
//
//var font = new Font(
//    baseFont, 
//    Font.DEFAULTSIZE,
//    Font.NORMAL)
//{
//    Size = 10
//};
//
//var fontBold = new Font(
//    baseFont, 
//    Font.BOLD,
//    Font.BOLD)
//{
//    Size = 10
//};
//
//var fontSmall = new Font(
//    baseFont, 
//    Font.DEFAULTSIZE,
//    Font.NORMAL)
//{
//    Size = 8
//};

//var document = new Document();
//
//var path = @$"{Directory.GetCurrentDirectory()}\{Guid.NewGuid().ToString()}";
//var stream = new FileStream(path, FileMode.Create);
//
//PdfWriter.GetInstance(document, stream);
//
//var rec = new Rectangle(PageSize.A4); 
//rec.BackgroundColor = new BaseColor(255, 255, 255); 
//document.SetPageSize(rec); 
//document.SetMargins(0, 0, 0, 0);
//
//document.Open();
//
//var table = new PdfPTable(2);
//var cells = new List<PdfPCell>()
//{
//    new(new Phrase("Nano35", font)),
//    new(new Phrase("BarCode image", font))
//    {
//        Rowspan = 3
//    },
//    new(new Phrase("+7(911)533 95 41", font)),
//    new(new Phrase("График работы: Пн-Пт с 9 до 19 Сб-Вс с 10 до 18", font)),
//    new(new Phrase("Квитанция No A001 от 07 декабря 2020", font))
//    {
//        Colspan = 2,
//        HorizontalAlignment = PdfPCell.ALIGN_CENTER
//    },
//    new(new ListItem()
//    {
//        new ListItem()
//        {
//            new Phrase("Клиент: ", fontBold),
//            new Phrase("Бушкова Н.В., +7 (333) 333-33-33", font)
//        },
//        new ListItem()
//        {
//            new Phrase("Серийный номер / IMEI: ", fontBold),
//            new Phrase("123123", font)
//        },
//        new ListItem()
//        {
//            new Phrase("Внешний вид: ", fontBold),
//            new Phrase("Царапины, потертости", font)
//        },
//    }),
//    new(new ListItem()
//    {
//        new ListItem()
//        {
//            new Phrase("Устройство: ", fontBold),
//            new Phrase("асус еее", font)
//        },
//        new ListItem()
//        {
//            new Phrase("Комплектация: ", fontBold),
//            new Phrase("З/У", font)
//        },
//        new ListItem()
//        {
//            new Phrase("Неисправность: ", fontBold),
//            new Phrase("Царапины, потертости", font)
//        },
//    }),
//    new(new Phrase("1. Сервисный центр не несет ответственности за возможную потерю " +
//                           "данных в памяти устройства, а также за оставленные SIM и FLASH " +
//                           "карты. Заблаговременно примите меры по резервированию информации.", fontSmall))
//    {
//        Colspan = 2
//    },
//    new(new Phrase("2. Заказчик принимает на себя риск возможной полной или частичной " +
//                           "утраты работоспособности устройства в процессе ремонта, в случае" +
//                           " грубых нарушений пользователем условий эксплуатации, наличия" +
//                           " следов попадания токопроводящей жидкости (коррозии), либо" +
//                           " механических повреждений.", fontSmall))
//    {
//        Colspan = 2
//    },
//    new(new Phrase("2.1 Заказчик согласен с возможными дополнительными неисправностями," +
//                           " которые могут быть выявлены во время ремонта или диагностики, и" +
//                           " берет на себя все финансовые обязательства.", fontSmall))
//    {
//        Colspan = 2
//    },
//    new(new Phrase("3. Гарантия не распространяется и не продлевается на устройства, " +
//                            "восстановленные после попадания жидкости.", fontSmall))
//    {
//        Colspan = 2
//    },
//    new(new Phrase("4. По истечению 30 дней, если заказчик не выходит на связь, техника " +
//                           "выставляется на продажу и реализуется.  Разница при продаже для" +
//                           " компенсации ремонтных работ уплачивается из общей суммы реализации," +
//                           " оставшаяся сумма возвращается заказчику.", fontSmall))
//    {
//        Colspan = 2,
//        Border = 0
//    },
//    new(new Phrase("5. В случае утери квитанции, устройство выдается по предъявлению" + 
//                            " паспорта на имя заказчика.", fontSmall))
//    {
//        Colspan = 2
//    },
//    new(new Phrase("6. Заказчик дает согласие на сбор и обработку его персональных" +
//                            " данных.", fontSmall))
//    {
//        Colspan = 2
//    },
//    new(new Phrase("Исполнитель: _____________________", font))
//    {
//        HorizontalAlignment = PdfPCell.ALIGN_LEFT
//    },
//    new(new Phrase("Заказчик: _____________________ Бушкова Н.В.с условиями ремонта ознакомлен и согласен ", font))
//    {
//        HorizontalAlignment = PdfPCell.ALIGN_RIGHT
//    },
//    new(new Phrase("--------------------------------------------------------------" +
//                   "--------------------------------------------------------------", font))
//    {
//        Colspan = 2,
//        HorizontalAlignment = PdfPCell.ALIGN_CENTER
//    },
//    new(new Phrase("BarCode", font))
//    {
//        Colspan = 2,
//        HorizontalAlignment = Element.ALIGN_RIGHT
//    },
//    new(new Phrase("Квитанция No A001 от 07 декабря 2020", font))
//    {
//        Colspan = 2,
//        HorizontalAlignment = PdfPCell.ALIGN_CENTER
//    },
//    new(new ListItem()
//    {
//        new ListItem()
//        {
//            new Phrase("Клиент: ", fontBold),
//            new Phrase("Бушкова Н.В., +7 (333) 333-33-33", font)
//        },
//        new ListItem()
//        {
//            new Phrase("Серийный номер / IMEI: ", fontBold),
//            new Phrase("123123", font)
//        },
//        new ListItem()
//        {
//            new Phrase("Внешний вид: ", fontBold),
//            new Phrase("Царапины, потертости", font)
//        },
//    }),
//    new(new ListItem()
//    {
//        new ListItem()
//        {
//            new Phrase("Устройство: ", fontBold),
//            new Phrase("асус еее", font)
//        },
//        new ListItem()
//        {
//            new Phrase("Комплектация: ", fontBold),
//            new Phrase("З/У", font)
//        },
//        new ListItem()
//        {
//            new Phrase("Неисправность: ", fontBold),
//            new Phrase("Царапины, потертости", font)
//        },
//    }),
//};
//
//cells.ForEach(e =>
//{
//    e.Border = 0;
//    table.AddCell(e);
//});
//
//document.Add(table);
//
//document.Close();

//const string mimeType = "application/pdf";
//return new FileStreamResult(new FileStream(path, FileMode.Open), mimeType);