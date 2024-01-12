using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Helpers;

public static class LogHelper
{
    public static string SerializeLogDetail(object logDetail)
    {
     
        return  JsonSerializer.Serialize(logDetail, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });


    }


}
