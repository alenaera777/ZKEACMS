/* http://www.zkea.net/ 
 * Copyright 2020 ZKEASOFT 
 * http://www.zkea.net/licenses */

using Easy;
using Easy.Extend;
using Easy.Cache;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Collections;

namespace ZKEACMS.Captcha
{
    public class ImageCaptchaService : IImageCaptchaService
    {
        private readonly IEnumerable<ICaptchaCodeStorageProvider> _captchaCodeStorageProviders;
        private readonly IImageGenerator _imageGenerator;
        public ImageCaptchaService(IImageGenerator imageGenerator, IEnumerable<ICaptchaCodeStorageProvider> captchaCodeStorageProviders)
        {
            _imageGenerator = imageGenerator;
            _captchaCodeStorageProviders = captchaCodeStorageProviders;
        }

        public byte[] GenerateCode(int num = 5)
        {
            string code = new RandomText().Generate(num);
            foreach (var item in _captchaCodeStorageProviders)
            {
                item.SaveCode(string.Empty, code);
            }
            return _imageGenerator.Generate(code);
        }

        public bool ValidateCode(string code)
        {
            if (code.IsNullOrWhiteSpace()) return false;

            return code.Equals(GetCode());
        }

        public string GetCode()
        {
            foreach (var item in _captchaCodeStorageProviders)
            {
                string code = item.GetCode(string.Empty);
                if (code.IsNotNullAndWhiteSpace()) return code;
            }
            return string.Empty;
        }
    }
}
