﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Blog_du_lịch.Models
{
    public class Service
    {
        private readonly string _dataFile = @"Data\data.xml";
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(HashSet<Travel>));
        public HashSet<Travel> Travels { get; set; }
        public Service()
        {
            if (!File.Exists(_dataFile))
            {
                Travels = new HashSet<Travel>()
                {
                    new Travel {Id=1, Name="Phố cổ Hội An", Adress="Quảng Nam, Việt Nam", Year=1999,
                    Descriptions="Hội An là một thành phố trực thuộc tỉnh Quảng Nam, Việt Nam. Phố cổ Hội An từng là một thương cảng quốc tế sầm uất, gồm những di sản kiến trúc đã có từ hàng trăm năm trước, được UNESCO công nhận là di sản văn hóa thế giới từ năm 1999.",
                    Emotions="Thích những lúc đi bộ dạo quanh khu phố đèn lồng đầy đủ sắc màu, thích ngắm nhìn những tòa nhà cổ kính trăm tuổi, ánh đèn lung linh trên sông Hoài, thích thưởng thức cao lầu và hến trộn ở cầu Cẩm Nam và thích luôn cả những âm vực nghe có vẻ nặng nề nhưng chứa đựng sự chân chất và mộc mạc đến lạ thường."
                    },
                    new Travel{Id=2, Name="Vịnh Hạ Long", Adress="Quảng Ninh,Việt Nam", Year=1994,
                    Descriptions="Vịnh Hạ Long là một vịnh nhỏ thuộc phần bờ tây vịnh Bắc Bộ tại khu vực biển Đông Bắc Việt Nam, bao gồm vùng biển đảo của thành phố Hạ Long thuộc tỉnh Quảng Ninh.",
                    Emotions="Một lần đến Vịnh hạ Long tôi đã tham quan và khám phá các Hòn Trống Mái, Hòn Con Cóc, Hang Sửng Sốt… trải nghiệm chèo thuyền kayak quanh các hòn đảo chiêm ngưỡng vẻ đẹp của rừng và biển. "
                    },
                    new Travel{Id=3, Name="Hồ Gươm", Adress="Hà Nội, Việt Nam",
                    Descriptions="Hồ Hoàn Kiếm còn được gọi là Hồ Gươm là một hồ nước ngọt tự nhiên nằm ở trung tâm thành phố Hà Nội. Hồ có diện tích khoảng 12 ha. Trước kia, hồ còn có các tên gọi là hồ Lục Thủy, hồ Thủy Quân, hồ Tả Vọng và Hữu Vọng",
                    Emotions="Hồ Gươm như một lẵng hoa xinh đẹp nằm giữa lòng thành phố. Sự tích cái tên Hồ Gươm hay còn gọi là hồ Hoàn Kiếm đã gắn liền với gần ngàn năm lịch sử của đất Thăng Long."
                    }
                };
                       
            }
            else
            {
                using var stream = File.OpenRead(_dataFile);
                Travels = _serializer.Deserialize(stream) as HashSet<Travel>;
            }
        }

        public Travel[] Get() => Travels.ToArray();
        public Travel Get(int id) => Travels.FirstOrDefault(s => s.Id == id);
        public bool Add(Travel travel) =>Travels.Add(travel);
        public Travel Create()
        {
            var m = Travels.Max(s => s.Id);
            var s = new Travel()
            {

                Id = m++
            };

            return s;
        }

        public bool Update(Travel travel)
        {
            var s = Get(travel.Id);
            return (s != null && Travels.Remove(s) && Travels.Add(travel));
        }

        public bool Delete(int id)
        {
            var s = Get(id);
            return (s != null && Travels.Remove(s));
        }

        public void SaveChanges()
        {
            using var stream = File.Create(_dataFile);
            _serializer.Serialize(stream, Travels);
        }


    }
}
