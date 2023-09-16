using ProniaBackEnd.Constants;

namespace ProniaBackEnd.Templates
{
    public class MessageTemplate
    {
        public const string APPROVED_ORDER_AZ = $"Hörmətli {OrderMessageTemplateKeywords.FIRSTNAME} {OrderMessageTemplateKeywords.LASTNAME}, sizin {OrderMessageTemplateKeywords.ORDER_NUMBER} təsdiqləndi.";
        public const string REJECTED_ORDER_AZ = $"Hörmətli {OrderMessageTemplateKeywords.FIRSTNAME} {OrderMessageTemplateKeywords.LASTNAME}, sizin {OrderMessageTemplateKeywords.ORDER_NUMBER}  təsdiqlənmədi.";
        public const string SENDED_ORDER_AZ = $"Hörmətli {OrderMessageTemplateKeywords.FIRSTNAME} {OrderMessageTemplateKeywords.LASTNAME}, sizin {OrderMessageTemplateKeywords.ORDER_NUMBER} göndərildi, kuryer sizinlə əlaqə saxlayacaq.";
        public const string CONFIRMED_ORDER_AZ = $" Hörmətli {OrderMessageTemplateKeywords.FIRSTNAME} {OrderMessageTemplateKeywords.LASTNAME}, sizin {OrderMessageTemplateKeywords.ORDER_NUMBER} kuryer tərəfindən təhvil verildi.";
        public const string CREATED_ORDER_AZ = $" Hörmətli {OrderMessageTemplateKeywords.FIRSTNAME} {OrderMessageTemplateKeywords.LASTNAME}, sizin {OrderMessageTemplateKeywords.ORDER_NUMBER} yaradıldı.";
        public const string COMPLETED_ORDER_AZ = $" Hörmətli {OrderMessageTemplateKeywords.FIRSTNAME} {OrderMessageTemplateKeywords.LASTNAME}, sizin {OrderMessageTemplateKeywords.ORDER_NUMBER} yaradıldı.";

        
    }
}
