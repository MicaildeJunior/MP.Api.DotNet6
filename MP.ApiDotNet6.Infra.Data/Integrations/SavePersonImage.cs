using MP.ApiDotNet6.Domain.Integrations;

namespace MP.ApiDotNet6.Infra.Data.Integrations;

public class SavePersonImage : ISavePersonImage
{
    private readonly string _filePath;
    public SavePersonImage()
    {
        _filePath = "/Users/micao/ImagemCursoApi";
    }

    // Implementção para o imageBase64 pegar qqr estenção, png, jpg etc...
    public string Save(string imageBase64)
    {
        // Pegando o imageBase64 e pegando qqr estenção dele
        var fileExt = imageBase64.Substring(imageBase64.IndexOf("/") + 1, 
                      imageBase64.IndexOf(";") - imageBase64.IndexOf("/") -1);

        // Remover o data do imageBase64
        var base64Code = imageBase64.Substring(imageBase64.IndexOf(",") + 1);

        // Convertendo pra byte
        var imgBytes = Convert.FromBase64String(base64Code);

        // Nomeando o arquivo
        var fileName = Guid.NewGuid().ToString() + "." + fileExt;
        
        // Salvando ele
        using(var imageFile = new FileStream(_filePath+"/"+fileName, FileMode.Create))
        {
            // Escrevendo nossa imagem,posição 0 que é de onde vai começar 'offset' até o limite 'Length'
            imageFile.Write(imgBytes,0,imgBytes.Length);
            // Limpando o FileStream
            imageFile.Flush();    
        }

        return _filePath + "/" + fileName;
    }
}
