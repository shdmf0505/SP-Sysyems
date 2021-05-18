#region using

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// 계수표 수식 추가. 1/8
    /// </summary>
    public static class CoeTable
    {
        public static List<Coe> coe = new List<Coe>();
        public static List<Coe> coed2 = new List<Coe>();
        public static List<Coe> coed3 = new List<Coe>();
        public static List<Coe> coeD3 = new List<Coe>();
        public static List<Coe> coed4 = new List<Coe>();
        public static List<Coe> coeD4 = new List<Coe>();
        public static List<Coe> coeA2 = new List<Coe>();
        public static List<Coe> coeA3 = new List<Coe>();
        public static List<Coe> coec5 = new List<Coe>();

        /// <summary>
        /// c4
        /// </summary>
        public static void Coeinitial()
        {
            if (coe != null && coe.Count > 0)
            {
                return;
            }

            coe.Add(new Coe("c4", 2, 0.7978846));
            coe.Add(new Coe("c4", 3, 0.886227));
            coe.Add(new Coe("c4", 4, 0.9213178));
            coe.Add(new Coe("c4", 5, 0.9399856));
            coe.Add(new Coe("c4", 6, 0.9515328));
            coe.Add(new Coe("c4", 7, 0.9593688));
            coe.Add(new Coe("c4", 8, 0.9650304));
            coe.Add(new Coe("c4", 9, 0.9693107));
            coe.Add(new Coe("c4", 10, 0.9726593));
            coe.Add(new Coe("c4", 11, 0.9753501));
            coe.Add(new Coe("c4", 12, 0.9775593));
            coe.Add(new Coe("c4", 13, 0.9794056));
            coe.Add(new Coe("c4", 14, 0.9809715));
            coe.Add(new Coe("c4", 15, 0.9823162));
            coe.Add(new Coe("c4", 16, 0.9834836));
            coe.Add(new Coe("c4", 17, 0.9845064));
            coe.Add(new Coe("c4", 18, 0.98541));
            coe.Add(new Coe("c4", 19, 0.9862142));
            coe.Add(new Coe("c4", 20, 0.9869342));
            coe.Add(new Coe("c4", 21, 0.9875829));
            coe.Add(new Coe("c4", 22, 0.9881703));
            coe.Add(new Coe("c4", 23, 0.9887046));
            coe.Add(new Coe("c4", 24, 0.9891927));
            coe.Add(new Coe("c4", 25, 0.9896404));
            coe.Add(new Coe("c4", 26, 0.9900525));
            coe.Add(new Coe("c4", 27, 0.990433));
            coe.Add(new Coe("c4", 28, 0.9907856));
            coe.Add(new Coe("c4", 29, 0.9911131));
            coe.Add(new Coe("c4", 30, 0.9914181));
            coe.Add(new Coe("c4", 31, 0.9917028));
            coe.Add(new Coe("c4", 32, 0.9919693));
            coe.Add(new Coe("c4", 33, 0.9922192));
            coe.Add(new Coe("c4", 34, 0.992454));
            coe.Add(new Coe("c4", 35, 0.9926751));
            coe.Add(new Coe("c4", 36, 0.9928836));
            coe.Add(new Coe("c4", 37, 0.9930805));
            coe.Add(new Coe("c4", 38, 0.9932668));
            coe.Add(new Coe("c4", 39, 0.9934434));
            coe.Add(new Coe("c4", 40, 0.9936109));
            coe.Add(new Coe("c4", 41, 0.9937701));
            coe.Add(new Coe("c4", 42, 0.9939216));
            coe.Add(new Coe("c4", 43, 0.9940659));
            coe.Add(new Coe("c4", 44, 0.9942034));
            coe.Add(new Coe("c4", 45, 0.9943348));
            coe.Add(new Coe("c4", 46, 0.9944603));
            coe.Add(new Coe("c4", 47, 0.9945804));
            coe.Add(new Coe("c4", 48, 0.9946954));
            coe.Add(new Coe("c4", 49, 0.9948056));
            coe.Add(new Coe("c4", 50, 0.9949113));
            coe.Add(new Coe("c4", 51, 0.9950128));
            coe.Add(new Coe("c4", 52, 0.9951103));
            coe.Add(new Coe("c4", 53, 0.9952042));
            coe.Add(new Coe("c4", 54, 0.9952944));
            coe.Add(new Coe("c4", 55, 0.9953814));
            coe.Add(new Coe("c4", 56, 0.9954651));
            coe.Add(new Coe("c4", 57, 0.9955459));
            coe.Add(new Coe("c4", 58, 0.9956239));
            coe.Add(new Coe("c4", 59, 0.9956992));
            coe.Add(new Coe("c4", 60, 0.9957719));
            coe.Add(new Coe("c4", 61, 0.9958422));
            coe.Add(new Coe("c4", 62, 0.9959102));
            coe.Add(new Coe("c4", 63, 0.995976));
            coe.Add(new Coe("c4", 64, 0.9960397));
            coe.Add(new Coe("c4", 65, 0.9961015));
            coe.Add(new Coe("c4", 66, 0.9961614));
            coe.Add(new Coe("c4", 67, 0.9962195));
            coe.Add(new Coe("c4", 68, 0.9962757));
            coe.Add(new Coe("c4", 69, 0.9963304));
            coe.Add(new Coe("c4", 70, 0.9963835));
            coe.Add(new Coe("c4", 71, 0.996435));
            coe.Add(new Coe("c4", 72, 0.9964852));
            coe.Add(new Coe("c4", 73, 0.9965339));
            coe.Add(new Coe("c4", 74, 0.9965813));
            coe.Add(new Coe("c4", 75, 0.9966274));
            coe.Add(new Coe("c4", 76, 0.9966723));
            coe.Add(new Coe("c4", 77, 0.996716));
            coe.Add(new Coe("c4", 78, 0.9967586));
            coe.Add(new Coe("c4", 79, 0.9968001));
            coe.Add(new Coe("c4", 80, 0.9968405));
            coe.Add(new Coe("c4", 81, 0.9968799));
            coe.Add(new Coe("c4", 82, 0.9969184));
            coe.Add(new Coe("c4", 83, 0.9969559));
            coe.Add(new Coe("c4", 84, 0.9969925));
            coe.Add(new Coe("c4", 85, 0.9970283));
            coe.Add(new Coe("c4", 86, 0.9970632));
            coe.Add(new Coe("c4", 87, 0.9970973));
            coe.Add(new Coe("c4", 88, 0.9971306));
            coe.Add(new Coe("c4", 89, 0.9971632));
            coe.Add(new Coe("c4", 90, 0.997195));
            coe.Add(new Coe("c4", 91, 0.9972261));
            coe.Add(new Coe("c4", 92, 0.9972566));
            coe.Add(new Coe("c4", 93, 0.9972864));
            coe.Add(new Coe("c4", 94, 0.9973155));
            coe.Add(new Coe("c4", 95, 0.997344));
            coe.Add(new Coe("c4", 96, 0.9973719));
            coe.Add(new Coe("c4", 97, 0.9973993));
            coe.Add(new Coe("c4", 98, 0.997426));
            coe.Add(new Coe("c4", 99, 0.9974523));
            coe.Add(new Coe("c4", 100, 0.9974779));
            coe.Add(new Coe("c4", 101, 0.9975032));
            coe.Add(new Coe("c4", 102, 0.9975278));
            coe.Add(new Coe("c4", 103, 0.997552));
            coe.Add(new Coe("c4", 104, 0.9975758));
            coe.Add(new Coe("c4", 105, 0.9975991));
            coe.Add(new Coe("c4", 106, 0.9976219));
            coe.Add(new Coe("c4", 107, 0.9976443));
            coe.Add(new Coe("c4", 108, 0.9976663));
            coe.Add(new Coe("c4", 109, 0.9976879));
            coe.Add(new Coe("c4", 110, 0.9977091));
            coe.Add(new Coe("c4", 111, 0.9977299));
            coe.Add(new Coe("c4", 112, 0.9977503));
            coe.Add(new Coe("c4", 113, 0.9977704));
            coe.Add(new Coe("c4", 114, 0.9977901));
            coe.Add(new Coe("c4", 115, 0.9978095));
            coe.Add(new Coe("c4", 116, 0.9978285));
            coe.Add(new Coe("c4", 117, 0.9978472));
            coe.Add(new Coe("c4", 118, 0.9978656));
            coe.Add(new Coe("c4", 119, 0.9978836));
            coe.Add(new Coe("c4", 120, 0.9979014));
            coe.Add(new Coe("c4", 121, 0.9979188));
            coe.Add(new Coe("c4", 122, 0.9979361));
            coe.Add(new Coe("c4", 123, 0.9979529));
            coe.Add(new Coe("c4", 124, 0.9979696));
            coe.Add(new Coe("c4", 125, 0.9979859));
            coe.Add(new Coe("c4", 126, 0.998002));
            coe.Add(new Coe("c4", 127, 0.9980178));
            coe.Add(new Coe("c4", 128, 0.9980335));
            coe.Add(new Coe("c4", 129, 0.9980488));
            coe.Add(new Coe("c4", 130, 0.9980639));
            coe.Add(new Coe("c4", 131, 0.9980788));
            coe.Add(new Coe("c4", 132, 0.9980934));
            coe.Add(new Coe("c4", 133, 0.9981079));
            coe.Add(new Coe("c4", 134, 0.9981221));
            coe.Add(new Coe("c4", 135, 0.9981361));
            coe.Add(new Coe("c4", 136, 0.9981499));
            coe.Add(new Coe("c4", 137, 0.9981635));
            coe.Add(new Coe("c4", 138, 0.9981769));
            coe.Add(new Coe("c4", 139, 0.99819));
            coe.Add(new Coe("c4", 140, 0.9982031));
            coe.Add(new Coe("c4", 141, 0.9982159));
            coe.Add(new Coe("c4", 142, 0.9982285));
            coe.Add(new Coe("c4", 143, 0.998241));
            coe.Add(new Coe("c4", 144, 0.9982533));
            coe.Add(new Coe("c4", 145, 0.9982654));
            coe.Add(new Coe("c4", 146, 0.9982774));
            coe.Add(new Coe("c4", 147, 0.9982892));
            coe.Add(new Coe("c4", 148, 0.9983008));
            coe.Add(new Coe("c4", 149, 0.9983122));
            coe.Add(new Coe("c4", 150, 0.9983236));
            coe.Add(new Coe("c4", 151, 0.9983347));
            coe.Add(new Coe("c4", 152, 0.9983457));
            coe.Add(new Coe("c4", 153, 0.9983566));
            coe.Add(new Coe("c4", 154, 0.9983674));
            coe.Add(new Coe("c4", 155, 0.998378));
            coe.Add(new Coe("c4", 156, 0.9983884));
            coe.Add(new Coe("c4", 157, 0.9983987));
            coe.Add(new Coe("c4", 158, 0.9984089));
            coe.Add(new Coe("c4", 159, 0.998419));
            coe.Add(new Coe("c4", 160, 0.9984289));
            coe.Add(new Coe("c4", 161, 0.9984387));
            coe.Add(new Coe("c4", 162, 0.9984484));
            coe.Add(new Coe("c4", 163, 0.998458));
            coe.Add(new Coe("c4", 164, 0.9984674));
            coe.Add(new Coe("c4", 165, 0.9984768));
            coe.Add(new Coe("c4", 166, 0.998486));
            coe.Add(new Coe("c4", 167, 0.9984951));
            coe.Add(new Coe("c4", 168, 0.9985041));
            coe.Add(new Coe("c4", 169, 0.998513));
            coe.Add(new Coe("c4", 170, 0.9985218));
            coe.Add(new Coe("c4", 171, 0.9985305));
            coe.Add(new Coe("c4", 172, 0.9985391));
            coe.Add(new Coe("c4", 173, 0.9985476));
            coe.Add(new Coe("c4", 174, 0.998556));
            coe.Add(new Coe("c4", 175, 0.9985642));
            coe.Add(new Coe("c4", 176, 0.9985725));
            coe.Add(new Coe("c4", 177, 0.9985806));
            coe.Add(new Coe("c4", 178, 0.9985886));
            coe.Add(new Coe("c4", 179, 0.9985965));
            coe.Add(new Coe("c4", 180, 0.9986044));
            coe.Add(new Coe("c4", 181, 0.9986121));
            coe.Add(new Coe("c4", 182, 0.9986197));
            coe.Add(new Coe("c4", 183, 0.9986273));
            coe.Add(new Coe("c4", 184, 0.9986348));
            coe.Add(new Coe("c4", 185, 0.9986422));
            coe.Add(new Coe("c4", 186, 0.9986496));
            coe.Add(new Coe("c4", 187, 0.9986568));
            coe.Add(new Coe("c4", 188, 0.998664));
            coe.Add(new Coe("c4", 189, 0.9986711));
            coe.Add(new Coe("c4", 190, 0.9986781));
            coe.Add(new Coe("c4", 191, 0.9986851));
            coe.Add(new Coe("c4", 192, 0.998692));
            coe.Add(new Coe("c4", 193, 0.9986988));
            coe.Add(new Coe("c4", 194, 0.9987055));
            coe.Add(new Coe("c4", 195, 0.9987122));
            coe.Add(new Coe("c4", 196, 0.9987188));
            coe.Add(new Coe("c4", 197, 0.9987253));
            coe.Add(new Coe("c4", 198, 0.9987318));
            coe.Add(new Coe("c4", 199, 0.9987382));
            coe.Add(new Coe("c4", 200, 0.9987445));
            coe.Add(new Coe("c4", 201, 0.9987508));
            coe.Add(new Coe("c4", 202, 0.998757));
            coe.Add(new Coe("c4", 203, 0.9987631));
            coe.Add(new Coe("c4", 204, 0.9987692));
            coe.Add(new Coe("c4", 205, 0.9987752));
            coe.Add(new Coe("c4", 206, 0.9987813));
            coe.Add(new Coe("c4", 207, 0.9987872));
            coe.Add(new Coe("c4", 208, 0.998793));
            coe.Add(new Coe("c4", 209, 0.9987988));
            coe.Add(new Coe("c4", 210, 0.9988046));
            coe.Add(new Coe("c4", 211, 0.9988102));
            coe.Add(new Coe("c4", 212, 0.9988159));
            coe.Add(new Coe("c4", 213, 0.9988214));
            coe.Add(new Coe("c4", 214, 0.998827));
            coe.Add(new Coe("c4", 215, 0.9988325));
            coe.Add(new Coe("c4", 216, 0.9988379));
            coe.Add(new Coe("c4", 217, 0.9988433));
            coe.Add(new Coe("c4", 218, 0.9988486));
            coe.Add(new Coe("c4", 219, 0.9988539));
            coe.Add(new Coe("c4", 220, 0.9988591));
            coe.Add(new Coe("c4", 221, 0.9988643));
            coe.Add(new Coe("c4", 222, 0.9988694));
            coe.Add(new Coe("c4", 223, 0.9988745));
            coe.Add(new Coe("c4", 224, 0.9988796));
            coe.Add(new Coe("c4", 225, 0.9988846));
            coe.Add(new Coe("c4", 226, 0.9988895));
            coe.Add(new Coe("c4", 227, 0.9988944));
            coe.Add(new Coe("c4", 228, 0.9988993));
            coe.Add(new Coe("c4", 229, 0.9989041));
            coe.Add(new Coe("c4", 230, 0.9989089));
            coe.Add(new Coe("c4", 231, 0.9989136));
            coe.Add(new Coe("c4", 232, 0.9989184));
            coe.Add(new Coe("c4", 233, 0.998923));
            coe.Add(new Coe("c4", 234, 0.9989276));
            coe.Add(new Coe("c4", 235, 0.9989322));
            coe.Add(new Coe("c4", 236, 0.9989367));
            coe.Add(new Coe("c4", 237, 0.9989412));
            coe.Add(new Coe("c4", 238, 0.9989457));
            coe.Add(new Coe("c4", 239, 0.9989501));
            coe.Add(new Coe("c4", 240, 0.9989545));
            coe.Add(new Coe("c4", 241, 0.9989589));
            coe.Add(new Coe("c4", 242, 0.9989632));
            coe.Add(new Coe("c4", 243, 0.9989675));
            coe.Add(new Coe("c4", 244, 0.9989717));
            coe.Add(new Coe("c4", 245, 0.9989759));
            coe.Add(new Coe("c4", 246, 0.9989801));
            coe.Add(new Coe("c4", 247, 0.9989843));
            coe.Add(new Coe("c4", 248, 0.9989884));
            coe.Add(new Coe("c4", 249, 0.9989924));
            coe.Add(new Coe("c4", 250, 0.9989965));
            coe.Add(new Coe("c4", 251, 0.9990005));
            coe.Add(new Coe("c4", 252, 0.9990045));
            coe.Add(new Coe("c4", 253, 0.9990084));
            coe.Add(new Coe("c4", 254, 0.9990124));
            coe.Add(new Coe("c4", 255, 0.9990162));
            coe.Add(new Coe("c4", 256, 0.9990201));
            coe.Add(new Coe("c4", 257, 0.9990239));
            coe.Add(new Coe("c4", 258, 0.9990277));
            coe.Add(new Coe("c4", 259, 0.9990315));
            coe.Add(new Coe("c4", 260, 0.9990352));
            coe.Add(new Coe("c4", 261, 0.9990389));
            coe.Add(new Coe("c4", 262, 0.9990426));
            coe.Add(new Coe("c4", 263, 0.9990463));
            coe.Add(new Coe("c4", 264, 0.9990499));
            coe.Add(new Coe("c4", 265, 0.9990535));
            coe.Add(new Coe("c4", 266, 0.9990571));
            coe.Add(new Coe("c4", 267, 0.9990606));
            coe.Add(new Coe("c4", 268, 0.9990641));
            coe.Add(new Coe("c4", 269, 0.9990676));
            coe.Add(new Coe("c4", 270, 0.9990711));
            coe.Add(new Coe("c4", 271, 0.9990745));
            coe.Add(new Coe("c4", 272, 0.9990779));
            coe.Add(new Coe("c4", 273, 0.9990813));
            coe.Add(new Coe("c4", 274, 0.9990847));
            coe.Add(new Coe("c4", 275, 0.999088));
            coe.Add(new Coe("c4", 276, 0.9990913));
            coe.Add(new Coe("c4", 277, 0.9990946));
            coe.Add(new Coe("c4", 278, 0.9990979));
            coe.Add(new Coe("c4", 279, 0.9991011));
            coe.Add(new Coe("c4", 280, 0.9991043));
            coe.Add(new Coe("c4", 281, 0.9991075));
            coe.Add(new Coe("c4", 282, 0.9991107));
            coe.Add(new Coe("c4", 283, 0.9991139));
            coe.Add(new Coe("c4", 284, 0.999117));
            coe.Add(new Coe("c4", 285, 0.9991201));
            coe.Add(new Coe("c4", 286, 0.9991232));
            coe.Add(new Coe("c4", 287, 0.9991263));
            coe.Add(new Coe("c4", 288, 0.9991293));
            coe.Add(new Coe("c4", 289, 0.9991323));
            coe.Add(new Coe("c4", 290, 0.9991353));
            coe.Add(new Coe("c4", 291, 0.9991383));
            coe.Add(new Coe("c4", 292, 0.9991413));
            coe.Add(new Coe("c4", 293, 0.9991442));
            coe.Add(new Coe("c4", 294, 0.9991471));
            coe.Add(new Coe("c4", 295, 0.99915));
            coe.Add(new Coe("c4", 296, 0.9991529));
            coe.Add(new Coe("c4", 297, 0.9991558));
            coe.Add(new Coe("c4", 298, 0.9991586));
            coe.Add(new Coe("c4", 299, 0.9991614));
            coe.Add(new Coe("c4", 300, 0.9991642));
            coe.Add(new Coe("c4", 301, 0.999167));
            coe.Add(new Coe("c4", 302, 0.9991698));
            coe.Add(new Coe("c4", 303, 0.9991725));
            coe.Add(new Coe("c4", 304, 0.9991753));
            coe.Add(new Coe("c4", 305, 0.999178));
            coe.Add(new Coe("c4", 306, 0.9991807));
            coe.Add(new Coe("c4", 307, 0.9991834));
            coe.Add(new Coe("c4", 308, 0.999186));
            coe.Add(new Coe("c4", 309, 0.9991887));
            coe.Add(new Coe("c4", 310, 0.9991913));
            coe.Add(new Coe("c4", 311, 0.9991938));
            coe.Add(new Coe("c4", 312, 0.9991965));
            coe.Add(new Coe("c4", 313, 0.999199));
            coe.Add(new Coe("c4", 314, 0.9992016));
            coe.Add(new Coe("c4", 315, 0.9992042));
            coe.Add(new Coe("c4", 316, 0.9992067));
            coe.Add(new Coe("c4", 317, 0.9992092));
            coe.Add(new Coe("c4", 318, 0.9992117));
            coe.Add(new Coe("c4", 319, 0.9992142));
            coe.Add(new Coe("c4", 320, 0.9992166));
            coe.Add(new Coe("c4", 321, 0.9992191));
            coe.Add(new Coe("c4", 322, 0.9992215));
            coe.Add(new Coe("c4", 323, 0.9992239));
            coe.Add(new Coe("c4", 324, 0.9992263));
            coe.Add(new Coe("c4", 325, 0.9992287));
            coe.Add(new Coe("c4", 326, 0.999231));
            coe.Add(new Coe("c4", 327, 0.9992334));
            coe.Add(new Coe("c4", 328, 0.9992357));
            coe.Add(new Coe("c4", 329, 0.9992381));
            coe.Add(new Coe("c4", 330, 0.9992404));
            coe.Add(new Coe("c4", 331, 0.9992427));
            coe.Add(new Coe("c4", 332, 0.999245));
            coe.Add(new Coe("c4", 333, 0.9992473));
            coe.Add(new Coe("c4", 334, 0.9992495));
            coe.Add(new Coe("c4", 335, 0.9992518));
            coe.Add(new Coe("c4", 336, 0.999254));
            coe.Add(new Coe("c4", 337, 0.9992563));
            coe.Add(new Coe("c4", 338, 0.9992585));
            coe.Add(new Coe("c4", 339, 0.9992606));
            coe.Add(new Coe("c4", 340, 0.9992628));
            coe.Add(new Coe("c4", 341, 0.999265));
            coe.Add(new Coe("c4", 342, 0.9992672));
            coe.Add(new Coe("c4", 343, 0.9992692));
            coe.Add(new Coe("c4", 344, 0.9992714));
            coe.Add(new Coe("c4", 345, 0.9992735));
            coe.Add(new Coe("c4", 346, 0.9992756));
            coe.Add(new Coe("c4", 347, 0.9992777));
            coe.Add(new Coe("c4", 348, 0.9992798));
            coe.Add(new Coe("c4", 349, 0.9992819));
            coe.Add(new Coe("c4", 350, 0.9992839));
            coe.Add(new Coe("c4", 351, 0.999286));
            coe.Add(new Coe("c4", 352, 0.999288));
            coe.Add(new Coe("c4", 353, 0.99929));
            coe.Add(new Coe("c4", 354, 0.999292));
            coe.Add(new Coe("c4", 355, 0.999294));
            coe.Add(new Coe("c4", 356, 0.999296));
            coe.Add(new Coe("c4", 357, 0.999298));
            coe.Add(new Coe("c4", 358, 0.9992999));
            coe.Add(new Coe("c4", 359, 0.9993019));
            coe.Add(new Coe("c4", 360, 0.9993039));
            coe.Add(new Coe("c4", 361, 0.9993058));
            coe.Add(new Coe("c4", 362, 0.9993077));
            coe.Add(new Coe("c4", 363, 0.9993097));
            coe.Add(new Coe("c4", 364, 0.9993115));
            coe.Add(new Coe("c4", 365, 0.9993134));
            coe.Add(new Coe("c4", 366, 0.9993153));
            coe.Add(new Coe("c4", 367, 0.9993172));
            coe.Add(new Coe("c4", 368, 0.999319));
            coe.Add(new Coe("c4", 369, 0.9993209));
            coe.Add(new Coe("c4", 370, 0.9993227));
            coe.Add(new Coe("c4", 371, 0.9993246));
            coe.Add(new Coe("c4", 372, 0.9993263));
            coe.Add(new Coe("c4", 373, 0.9993282));
            coe.Add(new Coe("c4", 374, 0.99933));
            coe.Add(new Coe("c4", 375, 0.9993318));
            coe.Add(new Coe("c4", 376, 0.9993336));
            coe.Add(new Coe("c4", 377, 0.9993353));
            coe.Add(new Coe("c4", 378, 0.9993371));
            coe.Add(new Coe("c4", 379, 0.9993389));
            coe.Add(new Coe("c4", 380, 0.9993406));
            coe.Add(new Coe("c4", 381, 0.9993423));
            coe.Add(new Coe("c4", 382, 0.9993441));
            coe.Add(new Coe("c4", 383, 0.9993458));
            coe.Add(new Coe("c4", 384, 0.9993474));
            coe.Add(new Coe("c4", 385, 0.9993492));
            coe.Add(new Coe("c4", 386, 0.9993508));
            coe.Add(new Coe("c4", 387, 0.9993525));
            coe.Add(new Coe("c4", 388, 0.9993542));
            coe.Add(new Coe("c4", 389, 0.9993559));
            coe.Add(new Coe("c4", 390, 0.9993575));
            coe.Add(new Coe("c4", 391, 0.9993592));
            coe.Add(new Coe("c4", 392, 0.9993608));
            coe.Add(new Coe("c4", 393, 0.9993625));
            coe.Add(new Coe("c4", 394, 0.9993641));
            coe.Add(new Coe("c4", 395, 0.9993657));
            coe.Add(new Coe("c4", 396, 0.9993673));
            coe.Add(new Coe("c4", 397, 0.9993689));
            coe.Add(new Coe("c4", 398, 0.9993705));
            coe.Add(new Coe("c4", 399, 0.9993721));
            coe.Add(new Coe("c4", 400, 0.9993736));
            coe.Add(new Coe("c4", 401, 0.9993752));
            coe.Add(new Coe("c4", 402, 0.9993768));
            coe.Add(new Coe("c4", 403, 0.9993783));
            coe.Add(new Coe("c4", 404, 0.9993799));
            coe.Add(new Coe("c4", 405, 0.9993814));
            coe.Add(new Coe("c4", 406, 0.9993829));
            coe.Add(new Coe("c4", 407, 0.9993844));
            coe.Add(new Coe("c4", 408, 0.999386));
            coe.Add(new Coe("c4", 409, 0.9993874));
            coe.Add(new Coe("c4", 410, 0.9993889));
            coe.Add(new Coe("c4", 411, 0.9993904));
            coe.Add(new Coe("c4", 412, 0.9993919));
            coe.Add(new Coe("c4", 413, 0.9993934));
            coe.Add(new Coe("c4", 414, 0.9993948));
            coe.Add(new Coe("c4", 415, 0.9993963));
            coe.Add(new Coe("c4", 416, 0.9993978));
            coe.Add(new Coe("c4", 417, 0.9993992));
            coe.Add(new Coe("c4", 418, 0.9994007));
            coe.Add(new Coe("c4", 419, 0.9994021));
            coe.Add(new Coe("c4", 420, 0.9994035));
            coe.Add(new Coe("c4", 421, 0.999405));
            coe.Add(new Coe("c4", 422, 0.9994063));
            coe.Add(new Coe("c4", 423, 0.9994078));
            coe.Add(new Coe("c4", 424, 0.9994091));
            coe.Add(new Coe("c4", 425, 0.9994106));
            coe.Add(new Coe("c4", 426, 0.9994119));
            coe.Add(new Coe("c4", 427, 0.9994133));
            coe.Add(new Coe("c4", 428, 0.9994147));
            coe.Add(new Coe("c4", 429, 0.9994161));
            coe.Add(new Coe("c4", 430, 0.9994174));
            coe.Add(new Coe("c4", 431, 0.9994188));
            coe.Add(new Coe("c4", 432, 0.9994201));
            coe.Add(new Coe("c4", 433, 0.9994215));
            coe.Add(new Coe("c4", 434, 0.9994228));
            coe.Add(new Coe("c4", 435, 0.9994242));
            coe.Add(new Coe("c4", 436, 0.9994255));
            coe.Add(new Coe("c4", 437, 0.9994268));
            coe.Add(new Coe("c4", 438, 0.9994281));
            coe.Add(new Coe("c4", 439, 0.9994294));
            coe.Add(new Coe("c4", 440, 0.9994307));
            coe.Add(new Coe("c4", 441, 0.999432));
            coe.Add(new Coe("c4", 442, 0.9994333));
            coe.Add(new Coe("c4", 443, 0.9994345));
            coe.Add(new Coe("c4", 444, 0.9994358));
            coe.Add(new Coe("c4", 445, 0.9994371));
            coe.Add(new Coe("c4", 446, 0.9994383));
            coe.Add(new Coe("c4", 447, 0.9994396));
            coe.Add(new Coe("c4", 448, 0.9994408));
            coe.Add(new Coe("c4", 449, 0.9994421));
            coe.Add(new Coe("c4", 450, 0.9994434));
            coe.Add(new Coe("c4", 451, 0.9994446));
            coe.Add(new Coe("c4", 452, 0.9994459));
            coe.Add(new Coe("c4", 453, 0.999447));
            coe.Add(new Coe("c4", 454, 0.9994483));
            coe.Add(new Coe("c4", 455, 0.9994495));
            coe.Add(new Coe("c4", 456, 0.9994507));
            coe.Add(new Coe("c4", 457, 0.9994519));
            coe.Add(new Coe("c4", 458, 0.9994531));
            coe.Add(new Coe("c4", 459, 0.9994543));
            coe.Add(new Coe("c4", 460, 0.9994555));
            coe.Add(new Coe("c4", 461, 0.9994566));
            coe.Add(new Coe("c4", 462, 0.9994578));
            coe.Add(new Coe("c4", 463, 0.999459));
            coe.Add(new Coe("c4", 464, 0.9994602));
            coe.Add(new Coe("c4", 465, 0.9994614));
            coe.Add(new Coe("c4", 466, 0.9994625));
            coe.Add(new Coe("c4", 467, 0.9994637));
            coe.Add(new Coe("c4", 468, 0.9994648));
            coe.Add(new Coe("c4", 469, 0.9994659));
            coe.Add(new Coe("c4", 470, 0.9994671));
            coe.Add(new Coe("c4", 471, 0.9994682));
            coe.Add(new Coe("c4", 472, 0.9994693));
            coe.Add(new Coe("c4", 473, 0.9994705));
            coe.Add(new Coe("c4", 474, 0.9994716));
            coe.Add(new Coe("c4", 475, 0.9994727));
            coe.Add(new Coe("c4", 476, 0.9994738));
            coe.Add(new Coe("c4", 477, 0.9994749));
            coe.Add(new Coe("c4", 478, 0.999476));
            coe.Add(new Coe("c4", 479, 0.9994771));
            coe.Add(new Coe("c4", 480, 0.9994782));
            coe.Add(new Coe("c4", 481, 0.9994793));
            coe.Add(new Coe("c4", 482, 0.9994804));
            coe.Add(new Coe("c4", 483, 0.9994814));
            coe.Add(new Coe("c4", 484, 0.9994825));
            coe.Add(new Coe("c4", 485, 0.9994836));
            coe.Add(new Coe("c4", 486, 0.9994847));
            coe.Add(new Coe("c4", 487, 0.9994857));
            coe.Add(new Coe("c4", 488, 0.9994868));
            coe.Add(new Coe("c4", 489, 0.9994878));
            coe.Add(new Coe("c4", 490, 0.9994889));
            coe.Add(new Coe("c4", 491, 0.9994899));
            coe.Add(new Coe("c4", 492, 0.999491));
            coe.Add(new Coe("c4", 493, 0.999492));
            coe.Add(new Coe("c4", 494, 0.999493));
            coe.Add(new Coe("c4", 495, 0.9994941));
            coe.Add(new Coe("c4", 496, 0.9994951));
            coe.Add(new Coe("c4", 497, 0.9994961));
            coe.Add(new Coe("c4", 498, 0.9994971));
            coe.Add(new Coe("c4", 499, 0.9994981));
            coe.Add(new Coe("c4", 500, 0.9994991));
            coe.Add(new Coe("c4", 501, 0.9995001));
            coe.Add(new Coe("c4", 502, 0.9995011));
            coe.Add(new Coe("c4", 503, 0.9995021));
            coe.Add(new Coe("c4", 504, 0.9995031));
            coe.Add(new Coe("c4", 505, 0.9995041));
            coe.Add(new Coe("c4", 506, 0.9995051));
            coe.Add(new Coe("c4", 507, 0.9995061));
            coe.Add(new Coe("c4", 508, 0.999507));
            coe.Add(new Coe("c4", 509, 0.999508));
            coe.Add(new Coe("c4", 510, 0.999509));
            coe.Add(new Coe("c4", 511, 0.9995099));
            coe.Add(new Coe("c4", 512, 0.9995109));
            coe.Add(new Coe("c4", 513, 0.9995118));
            coe.Add(new Coe("c4", 514, 0.9995128));
            coe.Add(new Coe("c4", 515, 0.9995137));
            coe.Add(new Coe("c4", 516, 0.9995147));
            coe.Add(new Coe("c4", 517, 0.9995156));
            coe.Add(new Coe("c4", 518, 0.9995165));
            coe.Add(new Coe("c4", 519, 0.9995175));
            coe.Add(new Coe("c4", 520, 0.9995184));
            coe.Add(new Coe("c4", 521, 0.9995193));
            coe.Add(new Coe("c4", 522, 0.9995202));
            coe.Add(new Coe("c4", 523, 0.9995212));
            coe.Add(new Coe("c4", 524, 0.9995221));
            coe.Add(new Coe("c4", 525, 0.999523));
            coe.Add(new Coe("c4", 526, 0.9995239));
            coe.Add(new Coe("c4", 527, 0.9995248));
            coe.Add(new Coe("c4", 528, 0.9995257));
            coe.Add(new Coe("c4", 529, 0.9995266));
            coe.Add(new Coe("c4", 530, 0.9995275));
            coe.Add(new Coe("c4", 531, 0.9995284));
            coe.Add(new Coe("c4", 532, 0.9995293));
            coe.Add(new Coe("c4", 533, 0.9995302));
            coe.Add(new Coe("c4", 534, 0.9995311));
            coe.Add(new Coe("c4", 535, 0.9995319));
            coe.Add(new Coe("c4", 536, 0.9995328));
            coe.Add(new Coe("c4", 537, 0.9995337));
            coe.Add(new Coe("c4", 538, 0.9995345));
            coe.Add(new Coe("c4", 539, 0.9995354));
            coe.Add(new Coe("c4", 540, 0.9995363));
            coe.Add(new Coe("c4", 541, 0.9995372));
            coe.Add(new Coe("c4", 542, 0.999538));
            coe.Add(new Coe("c4", 543, 0.9995388));
            coe.Add(new Coe("c4", 544, 0.9995397));
            coe.Add(new Coe("c4", 545, 0.9995406));
            coe.Add(new Coe("c4", 546, 0.9995414));
            coe.Add(new Coe("c4", 547, 0.9995422));
            coe.Add(new Coe("c4", 548, 0.9995431));
            coe.Add(new Coe("c4", 549, 0.9995439));
            coe.Add(new Coe("c4", 550, 0.9995447));
            coe.Add(new Coe("c4", 551, 0.9995456));
            coe.Add(new Coe("c4", 552, 0.9995464));
            coe.Add(new Coe("c4", 553, 0.9995472));
            coe.Add(new Coe("c4", 554, 0.999548));
            coe.Add(new Coe("c4", 555, 0.9995489));
            coe.Add(new Coe("c4", 556, 0.9995496));
            coe.Add(new Coe("c4", 557, 0.9995505));
            coe.Add(new Coe("c4", 558, 0.9995513));
            coe.Add(new Coe("c4", 559, 0.9995521));
            coe.Add(new Coe("c4", 560, 0.9995528));
            coe.Add(new Coe("c4", 561, 0.9995537));
            coe.Add(new Coe("c4", 562, 0.9995545));
            coe.Add(new Coe("c4", 563, 0.9995552));
            coe.Add(new Coe("c4", 564, 0.9995561));
            coe.Add(new Coe("c4", 565, 0.9995568));
            coe.Add(new Coe("c4", 566, 0.9995576));
            coe.Add(new Coe("c4", 567, 0.9995584));
            coe.Add(new Coe("c4", 568, 0.9995592));
            coe.Add(new Coe("c4", 569, 0.9995599));
            coe.Add(new Coe("c4", 570, 0.9995607));
            coe.Add(new Coe("c4", 571, 0.9995615));
            coe.Add(new Coe("c4", 572, 0.9995623));
            coe.Add(new Coe("c4", 573, 0.999563));
            coe.Add(new Coe("c4", 574, 0.9995638));
            coe.Add(new Coe("c4", 575, 0.9995645));
            coe.Add(new Coe("c4", 576, 0.9995653));
            coe.Add(new Coe("c4", 577, 0.9995661));
            coe.Add(new Coe("c4", 578, 0.9995668));
            coe.Add(new Coe("c4", 579, 0.9995676));
            coe.Add(new Coe("c4", 580, 0.9995683));
            coe.Add(new Coe("c4", 581, 0.9995691));
            coe.Add(new Coe("c4", 582, 0.9995698));
            coe.Add(new Coe("c4", 583, 0.9995705));
            coe.Add(new Coe("c4", 584, 0.9995713));
            coe.Add(new Coe("c4", 585, 0.999572));
            coe.Add(new Coe("c4", 586, 0.9995728));
            coe.Add(new Coe("c4", 587, 0.9995735));
            coe.Add(new Coe("c4", 588, 0.9995742));
            coe.Add(new Coe("c4", 589, 0.9995749));
            coe.Add(new Coe("c4", 590, 0.9995756));
            coe.Add(new Coe("c4", 591, 0.9995764));
            coe.Add(new Coe("c4", 592, 0.9995771));
            coe.Add(new Coe("c4", 593, 0.9995778));
            coe.Add(new Coe("c4", 594, 0.9995785));
            coe.Add(new Coe("c4", 595, 0.9995792));
            coe.Add(new Coe("c4", 596, 0.9995799));
            coe.Add(new Coe("c4", 597, 0.9995806));
            coe.Add(new Coe("c4", 598, 0.9995813));
            coe.Add(new Coe("c4", 599, 0.9995821));
            coe.Add(new Coe("c4", 600, 0.9995827));
            coe.Add(new Coe("c4", 601, 0.9995834));
            coe.Add(new Coe("c4", 602, 0.9995841));
            coe.Add(new Coe("c4", 603, 0.9995848));
            coe.Add(new Coe("c4", 604, 0.9995855));
            coe.Add(new Coe("c4", 605, 0.9995862));
            coe.Add(new Coe("c4", 606, 0.9995869));
            coe.Add(new Coe("c4", 607, 0.9995875));
            coe.Add(new Coe("c4", 608, 0.9995883));
            coe.Add(new Coe("c4", 609, 0.9995889));
            coe.Add(new Coe("c4", 610, 0.9995896));
            coe.Add(new Coe("c4", 611, 0.9995903));
            coe.Add(new Coe("c4", 612, 0.9995909));
            coe.Add(new Coe("c4", 613, 0.9995916));
            coe.Add(new Coe("c4", 614, 0.9995922));
            coe.Add(new Coe("c4", 615, 0.9995929));
            coe.Add(new Coe("c4", 616, 0.9995936));
            coe.Add(new Coe("c4", 617, 0.9995942));
            coe.Add(new Coe("c4", 618, 0.9995949));
            coe.Add(new Coe("c4", 619, 0.9995955));
            coe.Add(new Coe("c4", 620, 0.9995962));
            coe.Add(new Coe("c4", 621, 0.9995968));
            coe.Add(new Coe("c4", 622, 0.9995975));
            coe.Add(new Coe("c4", 623, 0.9995981));
            coe.Add(new Coe("c4", 624, 0.9995988));
            coe.Add(new Coe("c4", 625, 0.9995995));
            coe.Add(new Coe("c4", 626, 0.9996001));
            coe.Add(new Coe("c4", 627, 0.9996007));
            coe.Add(new Coe("c4", 628, 0.9996014));
            coe.Add(new Coe("c4", 629, 0.999602));
            coe.Add(new Coe("c4", 630, 0.9996026));
            coe.Add(new Coe("c4", 631, 0.9996033));
            coe.Add(new Coe("c4", 632, 0.9996039));
            coe.Add(new Coe("c4", 633, 0.9996045));
            coe.Add(new Coe("c4", 634, 0.9996051));
            coe.Add(new Coe("c4", 635, 0.9996058));
            coe.Add(new Coe("c4", 636, 0.9996064));
            coe.Add(new Coe("c4", 637, 0.999607));
            coe.Add(new Coe("c4", 638, 0.9996076));
            coe.Add(new Coe("c4", 639, 0.9996082));
            coe.Add(new Coe("c4", 640, 0.9996088));
            coe.Add(new Coe("c4", 641, 0.9996095));
            coe.Add(new Coe("c4", 642, 0.9996101));
            coe.Add(new Coe("c4", 643, 0.9996107));
            coe.Add(new Coe("c4", 644, 0.9996113));
            coe.Add(new Coe("c4", 645, 0.9996119));
            coe.Add(new Coe("c4", 646, 0.9996125));
            coe.Add(new Coe("c4", 647, 0.9996131));
            coe.Add(new Coe("c4", 648, 0.9996137));
            coe.Add(new Coe("c4", 649, 0.9996143));
            coe.Add(new Coe("c4", 650, 0.9996149));
            coe.Add(new Coe("c4", 651, 0.9996154));
            coe.Add(new Coe("c4", 652, 0.999616));
            coe.Add(new Coe("c4", 653, 0.9996166));
            coe.Add(new Coe("c4", 654, 0.9996172));
            coe.Add(new Coe("c4", 655, 0.9996178));
            coe.Add(new Coe("c4", 656, 0.9996184));
            coe.Add(new Coe("c4", 657, 0.9996189));
            coe.Add(new Coe("c4", 658, 0.9996195));
            coe.Add(new Coe("c4", 659, 0.9996201));
            coe.Add(new Coe("c4", 660, 0.9996207));
            coe.Add(new Coe("c4", 661, 0.9996213));
            coe.Add(new Coe("c4", 662, 0.9996219));
            coe.Add(new Coe("c4", 663, 0.9996224));
            coe.Add(new Coe("c4", 664, 0.999623));
            coe.Add(new Coe("c4", 665, 0.9996235));
            coe.Add(new Coe("c4", 666, 0.9996241));
            coe.Add(new Coe("c4", 667, 0.9996247));
            coe.Add(new Coe("c4", 668, 0.9996253));
            coe.Add(new Coe("c4", 669, 0.9996258));
            coe.Add(new Coe("c4", 670, 0.9996264));
            coe.Add(new Coe("c4", 671, 0.9996269));
            coe.Add(new Coe("c4", 672, 0.9996275));
            coe.Add(new Coe("c4", 673, 0.9996281));
            coe.Add(new Coe("c4", 674, 0.9996286));
            coe.Add(new Coe("c4", 675, 0.9996291));
            coe.Add(new Coe("c4", 676, 0.9996297));
            coe.Add(new Coe("c4", 677, 0.9996303));
            coe.Add(new Coe("c4", 678, 0.9996308));
            coe.Add(new Coe("c4", 679, 0.9996313));
            coe.Add(new Coe("c4", 680, 0.9996319));
            coe.Add(new Coe("c4", 681, 0.9996324));
            coe.Add(new Coe("c4", 682, 0.999633));
            coe.Add(new Coe("c4", 683, 0.9996335));
            coe.Add(new Coe("c4", 684, 0.999634));
            coe.Add(new Coe("c4", 685, 0.9996346));
            coe.Add(new Coe("c4", 686, 0.9996351));
            coe.Add(new Coe("c4", 687, 0.9996356));
            coe.Add(new Coe("c4", 688, 0.9996362));
            coe.Add(new Coe("c4", 689, 0.9996367));
            coe.Add(new Coe("c4", 690, 0.9996372));
            coe.Add(new Coe("c4", 691, 0.9996377));
            coe.Add(new Coe("c4", 692, 0.9996383));
            coe.Add(new Coe("c4", 693, 0.9996388));
            coe.Add(new Coe("c4", 694, 0.9996393));
            coe.Add(new Coe("c4", 695, 0.9996398));
            coe.Add(new Coe("c4", 696, 0.9996403));
            coe.Add(new Coe("c4", 697, 0.9996409));
            coe.Add(new Coe("c4", 698, 0.9996414));
            coe.Add(new Coe("c4", 699, 0.9996419));
            coe.Add(new Coe("c4", 700, 0.9996424));
            coe.Add(new Coe("c4", 701, 0.9996429));
            coe.Add(new Coe("c4", 702, 0.9996434));
            coe.Add(new Coe("c4", 703, 0.9996439));
            coe.Add(new Coe("c4", 704, 0.9996445));
            coe.Add(new Coe("c4", 705, 0.9996449));
            coe.Add(new Coe("c4", 706, 0.9996455));
            coe.Add(new Coe("c4", 707, 0.9996459));
            coe.Add(new Coe("c4", 708, 0.9996465));
            coe.Add(new Coe("c4", 709, 0.999647));
            coe.Add(new Coe("c4", 710, 0.9996474));
            coe.Add(new Coe("c4", 711, 0.999648));
            coe.Add(new Coe("c4", 712, 0.9996485));
            coe.Add(new Coe("c4", 713, 0.9996489));
            coe.Add(new Coe("c4", 714, 0.9996494));
            coe.Add(new Coe("c4", 715, 0.9996499));
            coe.Add(new Coe("c4", 716, 0.9996504));
            coe.Add(new Coe("c4", 717, 0.9996509));
            coe.Add(new Coe("c4", 718, 0.9996514));
            coe.Add(new Coe("c4", 719, 0.9996518));
            coe.Add(new Coe("c4", 720, 0.9996523));
            coe.Add(new Coe("c4", 721, 0.9996529));
            coe.Add(new Coe("c4", 722, 0.9996533));
            coe.Add(new Coe("c4", 723, 0.9996538));
            coe.Add(new Coe("c4", 724, 0.9996543));
            coe.Add(new Coe("c4", 725, 0.9996548));
            coe.Add(new Coe("c4", 726, 0.9996552));
            coe.Add(new Coe("c4", 727, 0.9996557));
            coe.Add(new Coe("c4", 728, 0.9996562));
            coe.Add(new Coe("c4", 729, 0.9996567));
            coe.Add(new Coe("c4", 730, 0.9996571));
            coe.Add(new Coe("c4", 731, 0.9996576));
            coe.Add(new Coe("c4", 732, 0.999658));
            coe.Add(new Coe("c4", 733, 0.9996585));
            coe.Add(new Coe("c4", 734, 0.999659));
            coe.Add(new Coe("c4", 735, 0.9996595));
            coe.Add(new Coe("c4", 736, 0.9996599));
            coe.Add(new Coe("c4", 737, 0.9996604));
            coe.Add(new Coe("c4", 738, 0.9996608));
            coe.Add(new Coe("c4", 739, 0.9996613));
            coe.Add(new Coe("c4", 740, 0.9996617));
            coe.Add(new Coe("c4", 741, 0.9996622));
            coe.Add(new Coe("c4", 742, 0.9996627));
            coe.Add(new Coe("c4", 743, 0.9996631));
            coe.Add(new Coe("c4", 744, 0.9996636));
            coe.Add(new Coe("c4", 745, 0.999664));
            coe.Add(new Coe("c4", 746, 0.9996645));
            coe.Add(new Coe("c4", 747, 0.999665));
            coe.Add(new Coe("c4", 748, 0.9996654));
            coe.Add(new Coe("c4", 749, 0.9996659));
            coe.Add(new Coe("c4", 750, 0.9996663));
            coe.Add(new Coe("c4", 751, 0.9996668));
            coe.Add(new Coe("c4", 752, 0.9996672));
            coe.Add(new Coe("c4", 753, 0.9996676));
            coe.Add(new Coe("c4", 754, 0.9996681));
            coe.Add(new Coe("c4", 755, 0.9996685));
            coe.Add(new Coe("c4", 756, 0.999669));
            coe.Add(new Coe("c4", 757, 0.9996694));
            coe.Add(new Coe("c4", 758, 0.9996698));
            coe.Add(new Coe("c4", 759, 0.9996703));
            coe.Add(new Coe("c4", 760, 0.9996707));
            coe.Add(new Coe("c4", 761, 0.9996711));
            coe.Add(new Coe("c4", 762, 0.9996715));
            coe.Add(new Coe("c4", 763, 0.999672));
            coe.Add(new Coe("c4", 764, 0.9996724));
            coe.Add(new Coe("c4", 765, 0.9996728));
            coe.Add(new Coe("c4", 766, 0.9996732));
            coe.Add(new Coe("c4", 767, 0.9996737));
            coe.Add(new Coe("c4", 768, 0.9996741));
            coe.Add(new Coe("c4", 769, 0.9996746));
            coe.Add(new Coe("c4", 770, 0.999675));
            coe.Add(new Coe("c4", 771, 0.9996754));
            coe.Add(new Coe("c4", 772, 0.9996758));
            coe.Add(new Coe("c4", 773, 0.9996762));
            coe.Add(new Coe("c4", 774, 0.9996766));
            coe.Add(new Coe("c4", 775, 0.9996771));
            coe.Add(new Coe("c4", 776, 0.9996775));
            coe.Add(new Coe("c4", 777, 0.9996779));
            coe.Add(new Coe("c4", 778, 0.9996783));
            coe.Add(new Coe("c4", 779, 0.9996787));
            coe.Add(new Coe("c4", 780, 0.9996791));
            coe.Add(new Coe("c4", 781, 0.9996796));
            coe.Add(new Coe("c4", 782, 0.9996799));
            coe.Add(new Coe("c4", 783, 0.9996803));
            coe.Add(new Coe("c4", 784, 0.9996808));
            coe.Add(new Coe("c4", 785, 0.9996812));
            coe.Add(new Coe("c4", 786, 0.9996816));
            coe.Add(new Coe("c4", 787, 0.999682));
            coe.Add(new Coe("c4", 788, 0.9996824));
            coe.Add(new Coe("c4", 789, 0.9996828));
            coe.Add(new Coe("c4", 790, 0.9996832));
            coe.Add(new Coe("c4", 791, 0.9996836));
            coe.Add(new Coe("c4", 792, 0.999684));
            coe.Add(new Coe("c4", 793, 0.9996844));
            coe.Add(new Coe("c4", 794, 0.9996848));
            coe.Add(new Coe("c4", 795, 0.9996852));
            coe.Add(new Coe("c4", 796, 0.9996856));
            coe.Add(new Coe("c4", 797, 0.999686));
            coe.Add(new Coe("c4", 798, 0.9996864));
            coe.Add(new Coe("c4", 799, 0.9996868));
            coe.Add(new Coe("c4", 800, 0.9996871));
            coe.Add(new Coe("c4", 801, 0.9996876));
            coe.Add(new Coe("c4", 802, 0.9996879));
            coe.Add(new Coe("c4", 803, 0.9996883));
            coe.Add(new Coe("c4", 804, 0.9996887));
            coe.Add(new Coe("c4", 805, 0.9996891));
            coe.Add(new Coe("c4", 806, 0.9996895));
            coe.Add(new Coe("c4", 807, 0.9996899));
            coe.Add(new Coe("c4", 808, 0.9996902));
            coe.Add(new Coe("c4", 809, 0.9996907));
            coe.Add(new Coe("c4", 810, 0.999691));
            coe.Add(new Coe("c4", 811, 0.9996914));
            coe.Add(new Coe("c4", 812, 0.9996918));
            coe.Add(new Coe("c4", 813, 0.9996921));
            coe.Add(new Coe("c4", 814, 0.9996926));
            coe.Add(new Coe("c4", 815, 0.9996929));
            coe.Add(new Coe("c4", 816, 0.9996933));
            coe.Add(new Coe("c4", 817, 0.9996937));
            coe.Add(new Coe("c4", 818, 0.999694));
            coe.Add(new Coe("c4", 819, 0.9996944));
            coe.Add(new Coe("c4", 820, 0.9996948));
            coe.Add(new Coe("c4", 821, 0.9996952));
            coe.Add(new Coe("c4", 822, 0.9996955));
            coe.Add(new Coe("c4", 823, 0.9996959));
            coe.Add(new Coe("c4", 824, 0.9996963));
            coe.Add(new Coe("c4", 825, 0.9996967));
            coe.Add(new Coe("c4", 826, 0.999697));
            coe.Add(new Coe("c4", 827, 0.9996974));
            coe.Add(new Coe("c4", 828, 0.9996977));
            coe.Add(new Coe("c4", 829, 0.9996981));
            coe.Add(new Coe("c4", 830, 0.9996985));
            coe.Add(new Coe("c4", 831, 0.9996988));
            coe.Add(new Coe("c4", 832, 0.9996992));
            coe.Add(new Coe("c4", 833, 0.9996996));
            coe.Add(new Coe("c4", 834, 0.9997));
            coe.Add(new Coe("c4", 835, 0.9997003));
            coe.Add(new Coe("c4", 836, 0.9997007));
            coe.Add(new Coe("c4", 837, 0.999701));
            coe.Add(new Coe("c4", 838, 0.9997014));
            coe.Add(new Coe("c4", 839, 0.9997017));
            coe.Add(new Coe("c4", 840, 0.9997021));
            coe.Add(new Coe("c4", 841, 0.9997025));
            coe.Add(new Coe("c4", 842, 0.9997028));
            coe.Add(new Coe("c4", 843, 0.9997031));
            coe.Add(new Coe("c4", 844, 0.9997035));
            coe.Add(new Coe("c4", 845, 0.9997038));
            coe.Add(new Coe("c4", 846, 0.9997042));
            coe.Add(new Coe("c4", 847, 0.9997045));
            coe.Add(new Coe("c4", 848, 0.9997049));
            coe.Add(new Coe("c4", 849, 0.9997053));
            coe.Add(new Coe("c4", 850, 0.9997056));
            coe.Add(new Coe("c4", 851, 0.9997059));
            coe.Add(new Coe("c4", 852, 0.9997063));
            coe.Add(new Coe("c4", 853, 0.9997066));
            coe.Add(new Coe("c4", 854, 0.999707));
            coe.Add(new Coe("c4", 855, 0.9997073));
            coe.Add(new Coe("c4", 856, 0.9997076));
            coe.Add(new Coe("c4", 857, 0.999708));
            coe.Add(new Coe("c4", 858, 0.9997084));
            coe.Add(new Coe("c4", 859, 0.9997087));
            coe.Add(new Coe("c4", 860, 0.999709));
            coe.Add(new Coe("c4", 861, 0.9997094));
            coe.Add(new Coe("c4", 862, 0.9997097));
            coe.Add(new Coe("c4", 863, 0.99971));
            coe.Add(new Coe("c4", 864, 0.9997104));
            coe.Add(new Coe("c4", 865, 0.9997107));
            coe.Add(new Coe("c4", 866, 0.999711));
            coe.Add(new Coe("c4", 867, 0.9997113));
            coe.Add(new Coe("c4", 868, 0.9997117));
            coe.Add(new Coe("c4", 869, 0.999712));
            coe.Add(new Coe("c4", 870, 0.9997123));
            coe.Add(new Coe("c4", 871, 0.9997127));
            coe.Add(new Coe("c4", 872, 0.999713));
            coe.Add(new Coe("c4", 873, 0.9997134));
            coe.Add(new Coe("c4", 874, 0.9997137));
            coe.Add(new Coe("c4", 875, 0.999714));
            coe.Add(new Coe("c4", 876, 0.9997143));
            coe.Add(new Coe("c4", 877, 0.9997147));
            coe.Add(new Coe("c4", 878, 0.999715));
            coe.Add(new Coe("c4", 879, 0.9997153));
            coe.Add(new Coe("c4", 880, 0.9997156));
            coe.Add(new Coe("c4", 881, 0.9997159));
            coe.Add(new Coe("c4", 882, 0.9997163));
            coe.Add(new Coe("c4", 883, 0.9997166));
            coe.Add(new Coe("c4", 884, 0.9997169));
            coe.Add(new Coe("c4", 885, 0.9997172));
            coe.Add(new Coe("c4", 886, 0.9997175));
            coe.Add(new Coe("c4", 887, 0.9997179));
            coe.Add(new Coe("c4", 888, 0.9997182));
            coe.Add(new Coe("c4", 889, 0.9997185));
            coe.Add(new Coe("c4", 890, 0.9997188));
            coe.Add(new Coe("c4", 891, 0.9997191));
            coe.Add(new Coe("c4", 892, 0.9997194));
            coe.Add(new Coe("c4", 893, 0.9997198));
            coe.Add(new Coe("c4", 894, 0.9997201));
            coe.Add(new Coe("c4", 895, 0.9997204));
            coe.Add(new Coe("c4", 896, 0.9997207));
            coe.Add(new Coe("c4", 897, 0.9997211));
            coe.Add(new Coe("c4", 898, 0.9997213));
            coe.Add(new Coe("c4", 899, 0.9997216));
            coe.Add(new Coe("c4", 900, 0.9997219));
            coe.Add(new Coe("c4", 901, 0.9997222));
            coe.Add(new Coe("c4", 902, 0.9997225));
            coe.Add(new Coe("c4", 903, 0.9997229));
            coe.Add(new Coe("c4", 904, 0.9997232));
            coe.Add(new Coe("c4", 905, 0.9997235));
            coe.Add(new Coe("c4", 906, 0.9997238));
            coe.Add(new Coe("c4", 907, 0.9997241));
            coe.Add(new Coe("c4", 908, 0.9997244));
            coe.Add(new Coe("c4", 909, 0.9997247));
            coe.Add(new Coe("c4", 910, 0.999725));
            coe.Add(new Coe("c4", 911, 0.9997253));
            coe.Add(new Coe("c4", 912, 0.9997256));
            coe.Add(new Coe("c4", 913, 0.9997259));
            coe.Add(new Coe("c4", 914, 0.9997262));
            coe.Add(new Coe("c4", 915, 0.9997265));
            coe.Add(new Coe("c4", 916, 0.9997268));
            coe.Add(new Coe("c4", 917, 0.9997271));
            coe.Add(new Coe("c4", 918, 0.9997274));
            coe.Add(new Coe("c4", 919, 0.9997277));
            coe.Add(new Coe("c4", 920, 0.999728));
            coe.Add(new Coe("c4", 921, 0.9997283));
            coe.Add(new Coe("c4", 922, 0.9997286));
            coe.Add(new Coe("c4", 923, 0.9997289));
            coe.Add(new Coe("c4", 924, 0.9997292));
            coe.Add(new Coe("c4", 925, 0.9997295));
            coe.Add(new Coe("c4", 926, 0.9997298));
            coe.Add(new Coe("c4", 927, 0.9997301));
            coe.Add(new Coe("c4", 928, 0.9997303));
            coe.Add(new Coe("c4", 929, 0.9997306));
            coe.Add(new Coe("c4", 930, 0.9997309));
            coe.Add(new Coe("c4", 931, 0.9997312));
            coe.Add(new Coe("c4", 932, 0.9997315));
            coe.Add(new Coe("c4", 933, 0.9997318));
            coe.Add(new Coe("c4", 934, 0.9997321));
            coe.Add(new Coe("c4", 935, 0.9997324));
            coe.Add(new Coe("c4", 936, 0.9997327));
            coe.Add(new Coe("c4", 937, 0.999733));
            coe.Add(new Coe("c4", 938, 0.9997332));
            coe.Add(new Coe("c4", 939, 0.9997335));
            coe.Add(new Coe("c4", 940, 0.9997338));
            coe.Add(new Coe("c4", 941, 0.9997341));
            coe.Add(new Coe("c4", 942, 0.9997343));
            coe.Add(new Coe("c4", 943, 0.9997346));
            coe.Add(new Coe("c4", 944, 0.9997349));
            coe.Add(new Coe("c4", 945, 0.9997352));
            coe.Add(new Coe("c4", 946, 0.9997355));
            coe.Add(new Coe("c4", 947, 0.9997358));
            coe.Add(new Coe("c4", 948, 0.9997361));
            coe.Add(new Coe("c4", 949, 0.9997363));
            coe.Add(new Coe("c4", 950, 0.9997366));
            coe.Add(new Coe("c4", 951, 0.9997369));
            coe.Add(new Coe("c4", 952, 0.9997371));
            coe.Add(new Coe("c4", 953, 0.9997374));
            coe.Add(new Coe("c4", 954, 0.9997377));
            coe.Add(new Coe("c4", 955, 0.999738));
            coe.Add(new Coe("c4", 956, 0.9997383));
            coe.Add(new Coe("c4", 957, 0.9997385));
            coe.Add(new Coe("c4", 958, 0.9997388));
            coe.Add(new Coe("c4", 959, 0.9997391));
            coe.Add(new Coe("c4", 960, 0.9997393));
            coe.Add(new Coe("c4", 961, 0.9997396));
            coe.Add(new Coe("c4", 962, 0.9997399));
            coe.Add(new Coe("c4", 963, 0.9997402));
            coe.Add(new Coe("c4", 964, 0.9997404));
            coe.Add(new Coe("c4", 965, 0.9997407));
            coe.Add(new Coe("c4", 966, 0.999741));
            coe.Add(new Coe("c4", 967, 0.9997413));
            coe.Add(new Coe("c4", 968, 0.9997415));
            coe.Add(new Coe("c4", 969, 0.9997418));
            coe.Add(new Coe("c4", 970, 0.999742));
            coe.Add(new Coe("c4", 971, 0.9997423));
            coe.Add(new Coe("c4", 972, 0.9997426));
            coe.Add(new Coe("c4", 973, 0.9997428));
            coe.Add(new Coe("c4", 974, 0.9997431));
            coe.Add(new Coe("c4", 975, 0.9997433));
            coe.Add(new Coe("c4", 976, 0.9997436));
            coe.Add(new Coe("c4", 977, 0.9997439));
            coe.Add(new Coe("c4", 978, 0.9997441));
            coe.Add(new Coe("c4", 979, 0.9997444));
            coe.Add(new Coe("c4", 980, 0.9997447));
            coe.Add(new Coe("c4", 981, 0.999745));
            coe.Add(new Coe("c4", 982, 0.9997452));
            coe.Add(new Coe("c4", 983, 0.9997454));
            coe.Add(new Coe("c4", 984, 0.9997457));
            coe.Add(new Coe("c4", 985, 0.999746));
            coe.Add(new Coe("c4", 986, 0.9997462));
            coe.Add(new Coe("c4", 987, 0.9997465));
            coe.Add(new Coe("c4", 988, 0.9997467));
            coe.Add(new Coe("c4", 989, 0.999747));
            coe.Add(new Coe("c4", 990, 0.9997473));
            coe.Add(new Coe("c4", 991, 0.9997475));
            coe.Add(new Coe("c4", 992, 0.9997478));
            coe.Add(new Coe("c4", 993, 0.999748));
            coe.Add(new Coe("c4", 994, 0.9997483));
            coe.Add(new Coe("c4", 995, 0.9997485));
            coe.Add(new Coe("c4", 996, 0.9997488));
            coe.Add(new Coe("c4", 997, 0.999749));
            coe.Add(new Coe("c4", 998, 0.9997493));
            coe.Add(new Coe("c4", 999, 0.9997495));
            coe.Add(new Coe("c4", 1000, 0.9997498));
            coe.Add(new Coe("c4", 1001, 0.99975));
        }

        /// <summary>
        /// c4
        /// </summary>
        public static void CoeinitialMini()
        {
            if (coe != null && coe.Count > 0)
            {
                return;
            }

            coe.Add(new Coe("c4", 2, 0.79785));
            coe.Add(new Coe("c4", 3, 0.87153));
            coe.Add(new Coe("c4", 4, 0.905763));
            coe.Add(new Coe("c4", 5, 0.925222));
            coe.Add(new Coe("c4", 6, 0.937892));
            coe.Add(new Coe("c4", 7, 0.946837));
            coe.Add(new Coe("c4", 8, 0.953503));
            coe.Add(new Coe("c4", 9, 0.958669));
            coe.Add(new Coe("c4", 10, 0.962793));
            coe.Add(new Coe("c4", 11, 0.966163));
            coe.Add(new Coe("c4", 12, 0.968968));
            coe.Add(new Coe("c4", 13, 0.971341));
            coe.Add(new Coe("c4", 14, 0.973375));
            coe.Add(new Coe("c4", 15, 0.975137));
            coe.Add(new Coe("c4", 16, 0.976679));
            coe.Add(new Coe("c4", 17, 0.978039));
            coe.Add(new Coe("c4", 18, 0.979249));
            coe.Add(new Coe("c4", 19, 0.980331));
            coe.Add(new Coe("c4", 20, 0.981305));
            coe.Add(new Coe("c4", 21, 0.982187));
            coe.Add(new Coe("c4", 22, 0.982988));
            coe.Add(new Coe("c4", 23, 0.98372));
            coe.Add(new Coe("c4", 24, 0.984391));
            coe.Add(new Coe("c4", 25, 0.985009));
            coe.Add(new Coe("c4", 26, 0.985579));
            coe.Add(new Coe("c4", 27, 0.986107));
            coe.Add(new Coe("c4", 28, 0.986597));
            coe.Add(new Coe("c4", 29, 0.987054));
            coe.Add(new Coe("c4", 30, 0.98748));
            coe.Add(new Coe("c4", 31, 0.987878));
            coe.Add(new Coe("c4", 32, 0.988252));
            coe.Add(new Coe("c4", 33, 0.988603));
            coe.Add(new Coe("c4", 34, 0.988934));
            coe.Add(new Coe("c4", 35, 0.989246));
            coe.Add(new Coe("c4", 36, 0.98954));
            coe.Add(new Coe("c4", 37, 0.989819));
            coe.Add(new Coe("c4", 38, 0.990083));
            coe.Add(new Coe("c4", 39, 0.990333));
            coe.Add(new Coe("c4", 40, 0.990571));
            coe.Add(new Coe("c4", 41, 0.990797));
            coe.Add(new Coe("c4", 42, 0.991013));
            coe.Add(new Coe("c4", 43, 0.991218));
            coe.Add(new Coe("c4", 44, 0.991415));
            coe.Add(new Coe("c4", 45, 0.991602));
            coe.Add(new Coe("c4", 46, 0.991782));
            coe.Add(new Coe("c4", 47, 0.991953));
            coe.Add(new Coe("c4", 48, 0.992118));
            coe.Add(new Coe("c4", 49, 0.992276));
            coe.Add(new Coe("c4", 50, 0.992427));
            coe.Add(new Coe("c4", 51, 0.992573));
            coe.Add(new Coe("c4", 52, 0.992713));
            coe.Add(new Coe("c4", 53, 0.992848));
            coe.Add(new Coe("c4", 54, 0.992978));
            coe.Add(new Coe("c4", 55, 0.993103));
            coe.Add(new Coe("c4", 56, 0.993224));
            coe.Add(new Coe("c4", 57, 0.99334));
            coe.Add(new Coe("c4", 58, 0.993452));
            coe.Add(new Coe("c4", 59, 0.993561));
            coe.Add(new Coe("c4", 60, 0.993666));
            coe.Add(new Coe("c4", 61, 0.993767));
            coe.Add(new Coe("c4", 62, 0.993866));
            coe.Add(new Coe("c4", 63, 0.993961));
            coe.Add(new Coe("c4", 64, 0.994053));
            coe.Add(new Coe("c4", 65, 0.994142));
            coe.Add(new Coe("c4", 66, 0.994229));
            coe.Add(new Coe("c4", 67, 0.994313));
            coe.Add(new Coe("c4", 68, 0.994395));
            coe.Add(new Coe("c4", 69, 0.994474));
            coe.Add(new Coe("c4", 70, 0.994551));
            coe.Add(new Coe("c4", 71, 0.994626));
            coe.Add(new Coe("c4", 72, 0.994699));
            coe.Add(new Coe("c4", 73, 0.994769));
            coe.Add(new Coe("c4", 74, 0.994838));
            coe.Add(new Coe("c4", 75, 0.994905));
            coe.Add(new Coe("c4", 76, 0.99497));
            coe.Add(new Coe("c4", 77, 0.995034));
            coe.Add(new Coe("c4", 78, 0.995096));
            coe.Add(new Coe("c4", 79, 0.995156));
            coe.Add(new Coe("c4", 80, 0.995215));
            coe.Add(new Coe("c4", 81, 0.995272));
            coe.Add(new Coe("c4", 82, 0.995328));
            coe.Add(new Coe("c4", 83, 0.995383));
            coe.Add(new Coe("c4", 84, 0.995436));
            coe.Add(new Coe("c4", 85, 0.995489));
            coe.Add(new Coe("c4", 86, 0.995539));
            coe.Add(new Coe("c4", 87, 0.995589));
            coe.Add(new Coe("c4", 88, 0.995638));
            coe.Add(new Coe("c4", 89, 0.995685));
            coe.Add(new Coe("c4", 90, 0.995732));
            coe.Add(new Coe("c4", 91, 0.995777));
            coe.Add(new Coe("c4", 92, 0.995822));
            coe.Add(new Coe("c4", 93, 0.995865));
            coe.Add(new Coe("c4", 94, 0.995908));
            coe.Add(new Coe("c4", 95, 0.995949));
            coe.Add(new Coe("c4", 96, 0.99599));
            coe.Add(new Coe("c4", 97, 0.99603));
            coe.Add(new Coe("c4", 98, 0.996069));
            coe.Add(new Coe("c4", 99, 0.996108));
            coe.Add(new Coe("c4", 100, 0.996145));
            coe.Add(new Coe("c4", 101, 0.996182));
            coe.Add(new Coe("c4", 102, 0.996218));
            coe.Add(new Coe("c4", 103, 0.996253));
            coe.Add(new Coe("c4", 104, 0.996288));
            coe.Add(new Coe("c4", 105, 0.996322));
            coe.Add(new Coe("c4", 106, 0.996356));
            coe.Add(new Coe("c4", 107, 0.996389));
            coe.Add(new Coe("c4", 108, 0.996421));
            coe.Add(new Coe("c4", 109, 0.996452));
            coe.Add(new Coe("c4", 110, 0.996483));
            coe.Add(new Coe("c4", 111, 0.996514));
            coe.Add(new Coe("c4", 112, 0.996544));
            coe.Add(new Coe("c4", 113, 0.996573));
            coe.Add(new Coe("c4", 114, 0.996602));
            coe.Add(new Coe("c4", 115, 0.996631));
            coe.Add(new Coe("c4", 116, 0.996658));
            coe.Add(new Coe("c4", 117, 0.996686));
            coe.Add(new Coe("c4", 118, 0.996713));
            coe.Add(new Coe("c4", 119, 0.996739));
            coe.Add(new Coe("c4", 120, 0.996765));
            coe.Add(new Coe("c4", 121, 0.996791));
            coe.Add(new Coe("c4", 122, 0.996816));
            coe.Add(new Coe("c4", 123, 0.996841));
            coe.Add(new Coe("c4", 124, 0.996865));
            coe.Add(new Coe("c4", 125, 0.996889));
            coe.Add(new Coe("c4", 126, 0.996913));
            coe.Add(new Coe("c4", 127, 0.996936));
            coe.Add(new Coe("c4", 128, 0.996959));
            coe.Add(new Coe("c4", 129, 0.996982));
            coe.Add(new Coe("c4", 130, 0.997004));
            coe.Add(new Coe("c4", 131, 0.997026));
            coe.Add(new Coe("c4", 132, 0.997047));
            coe.Add(new Coe("c4", 133, 0.997069));
            coe.Add(new Coe("c4", 134, 0.997089));
            coe.Add(new Coe("c4", 135, 0.99711));
            coe.Add(new Coe("c4", 136, 0.99713));
            coe.Add(new Coe("c4", 137, 0.99715));
            coe.Add(new Coe("c4", 138, 0.99717));
            coe.Add(new Coe("c4", 139, 0.997189));
            coe.Add(new Coe("c4", 140, 0.997209));
            coe.Add(new Coe("c4", 141, 0.997227));
            coe.Add(new Coe("c4", 142, 0.997246));
            coe.Add(new Coe("c4", 143, 0.997264));
            coe.Add(new Coe("c4", 144, 0.997282));
            coe.Add(new Coe("c4", 145, 0.9973));
            coe.Add(new Coe("c4", 146, 0.997318));
            coe.Add(new Coe("c4", 147, 0.997335));
            coe.Add(new Coe("c4", 148, 0.997352));
            coe.Add(new Coe("c4", 149, 0.997369));
            coe.Add(new Coe("c4", 150, 0.997386));
            coe.Add(new Coe("c4", 151, 0.997402));
            coe.Add(new Coe("c4", 152, 0.997419));
            coe.Add(new Coe("c4", 153, 0.997435));
            coe.Add(new Coe("c4", 154, 0.99745));
            coe.Add(new Coe("c4", 155, 0.997466));
            coe.Add(new Coe("c4", 156, 0.997481));
            coe.Add(new Coe("c4", 157, 0.997497));
            coe.Add(new Coe("c4", 158, 0.997512));
            coe.Add(new Coe("c4", 159, 0.997526));
            coe.Add(new Coe("c4", 160, 0.997541));
            coe.Add(new Coe("c4", 161, 0.997555));
            coe.Add(new Coe("c4", 162, 0.99757));
            coe.Add(new Coe("c4", 163, 0.997584));
            coe.Add(new Coe("c4", 164, 0.997598));
            coe.Add(new Coe("c4", 165, 0.997612));
            coe.Add(new Coe("c4", 166, 0.997625));
            coe.Add(new Coe("c4", 167, 0.997639));
            coe.Add(new Coe("c4", 168, 0.997652));
            coe.Add(new Coe("c4", 169, 0.997665));
            coe.Add(new Coe("c4", 170, 0.997678));
            coe.Add(new Coe("c4", 171, 0.997691));
            coe.Add(new Coe("c4", 172, 0.997703));
            coe.Add(new Coe("c4", 173, 0.997716));
            coe.Add(new Coe("c4", 174, 0.997728));
            coe.Add(new Coe("c4", 175, 0.997741));
            coe.Add(new Coe("c4", 176, 0.997753));
            coe.Add(new Coe("c4", 177, 0.997765));
            coe.Add(new Coe("c4", 178, 0.997776));
            coe.Add(new Coe("c4", 179, 0.997788));
            coe.Add(new Coe("c4", 180, 0.9978));
            coe.Add(new Coe("c4", 181, 0.997811));
            coe.Add(new Coe("c4", 182, 0.997822));
            coe.Add(new Coe("c4", 183, 0.997834));
            coe.Add(new Coe("c4", 184, 0.997845));
            coe.Add(new Coe("c4", 185, 0.997856));
            coe.Add(new Coe("c4", 186, 0.997866));
            coe.Add(new Coe("c4", 187, 0.997877));
            coe.Add(new Coe("c4", 188, 0.997888));
            coe.Add(new Coe("c4", 189, 0.997898));
            coe.Add(new Coe("c4", 190, 0.997909));
            coe.Add(new Coe("c4", 191, 0.997919));
            coe.Add(new Coe("c4", 192, 0.997929));
            coe.Add(new Coe("c4", 193, 0.997939));
            coe.Add(new Coe("c4", 194, 0.997949));
            coe.Add(new Coe("c4", 195, 0.997959));
            coe.Add(new Coe("c4", 196, 0.997969));
            coe.Add(new Coe("c4", 197, 0.997978));
            coe.Add(new Coe("c4", 198, 0.997988));
            coe.Add(new Coe("c4", 199, 0.997997));
            coe.Add(new Coe("c4", 200, 0.998007));
            coe.Add(new Coe("c4", 201, 0.998016));
            coe.Add(new Coe("c4", 202, 0.998025));
            coe.Add(new Coe("c4", 203, 0.998034));
            coe.Add(new Coe("c4", 204, 0.998043));
            coe.Add(new Coe("c4", 205, 0.998052));
            coe.Add(new Coe("c4", 206, 0.998061));
            coe.Add(new Coe("c4", 207, 0.99807));
            coe.Add(new Coe("c4", 208, 0.998078));
            coe.Add(new Coe("c4", 209, 0.998087));
            coe.Add(new Coe("c4", 210, 0.998095));
            coe.Add(new Coe("c4", 211, 0.998104));
            coe.Add(new Coe("c4", 212, 0.998112));
            coe.Add(new Coe("c4", 213, 0.99812));
            coe.Add(new Coe("c4", 214, 0.998128));
            coe.Add(new Coe("c4", 215, 0.998137));
            coe.Add(new Coe("c4", 216, 0.998145));
            coe.Add(new Coe("c4", 217, 0.998152));
            coe.Add(new Coe("c4", 218, 0.99816));
            coe.Add(new Coe("c4", 219, 0.998168));
            coe.Add(new Coe("c4", 220, 0.998176));
            coe.Add(new Coe("c4", 221, 0.998184));
            coe.Add(new Coe("c4", 222, 0.998191));
            coe.Add(new Coe("c4", 223, 0.998199));
            coe.Add(new Coe("c4", 224, 0.998206));
            coe.Add(new Coe("c4", 225, 0.998214));
            coe.Add(new Coe("c4", 226, 0.998221));
            coe.Add(new Coe("c4", 227, 0.998228));
            coe.Add(new Coe("c4", 228, 0.998235));
            coe.Add(new Coe("c4", 229, 0.998242));
            coe.Add(new Coe("c4", 230, 0.99825));
            coe.Add(new Coe("c4", 231, 0.998257));
            coe.Add(new Coe("c4", 232, 0.998263));
            coe.Add(new Coe("c4", 233, 0.99827));
            coe.Add(new Coe("c4", 234, 0.998277));
            coe.Add(new Coe("c4", 235, 0.998284));
            coe.Add(new Coe("c4", 236, 0.998291));
            coe.Add(new Coe("c4", 237, 0.998297));
            coe.Add(new Coe("c4", 238, 0.998304));
            coe.Add(new Coe("c4", 239, 0.998311));
            coe.Add(new Coe("c4", 240, 0.998317));
            coe.Add(new Coe("c4", 241, 0.998323));
            coe.Add(new Coe("c4", 242, 0.99833));
            coe.Add(new Coe("c4", 243, 0.998336));
            coe.Add(new Coe("c4", 244, 0.998342));
            coe.Add(new Coe("c4", 245, 0.998349));
            coe.Add(new Coe("c4", 246, 0.998355));
            coe.Add(new Coe("c4", 247, 0.998361));
            coe.Add(new Coe("c4", 248, 0.998367));
            coe.Add(new Coe("c4", 249, 0.998373));
            coe.Add(new Coe("c4", 250, 0.998379));
            coe.Add(new Coe("c4", 251, 0.998385));
            coe.Add(new Coe("c4", 252, 0.998391));
            coe.Add(new Coe("c4", 253, 0.998397));
            coe.Add(new Coe("c4", 254, 0.998403));
            coe.Add(new Coe("c4", 255, 0.998408));
            coe.Add(new Coe("c4", 256, 0.998414));
            coe.Add(new Coe("c4", 257, 0.99842));
            coe.Add(new Coe("c4", 258, 0.998425));
            coe.Add(new Coe("c4", 259, 0.998431));
            coe.Add(new Coe("c4", 260, 0.998436));
            coe.Add(new Coe("c4", 261, 0.998442));
            coe.Add(new Coe("c4", 262, 0.998447));
            coe.Add(new Coe("c4", 263, 0.998453));
            coe.Add(new Coe("c4", 264, 0.998458));
            coe.Add(new Coe("c4", 265, 0.998463));
            coe.Add(new Coe("c4", 266, 0.998469));
            coe.Add(new Coe("c4", 267, 0.998474));
            coe.Add(new Coe("c4", 268, 0.998479));
            coe.Add(new Coe("c4", 269, 0.998484));
            coe.Add(new Coe("c4", 270, 0.998489));
            coe.Add(new Coe("c4", 271, 0.998495));
            coe.Add(new Coe("c4", 272, 0.9985));
            coe.Add(new Coe("c4", 273, 0.998505));
            coe.Add(new Coe("c4", 274, 0.99851));
            coe.Add(new Coe("c4", 275, 0.998515));
            coe.Add(new Coe("c4", 276, 0.998519));
            coe.Add(new Coe("c4", 277, 0.998524));
            coe.Add(new Coe("c4", 278, 0.998529));
            coe.Add(new Coe("c4", 279, 0.998534));
            coe.Add(new Coe("c4", 280, 0.998539));
            coe.Add(new Coe("c4", 281, 0.998544));
            coe.Add(new Coe("c4", 282, 0.998548));
            coe.Add(new Coe("c4", 283, 0.998553));
            coe.Add(new Coe("c4", 284, 0.998558));
            coe.Add(new Coe("c4", 285, 0.998562));
            coe.Add(new Coe("c4", 286, 0.998567));
            coe.Add(new Coe("c4", 287, 0.998571));
            coe.Add(new Coe("c4", 288, 0.998576));
            coe.Add(new Coe("c4", 289, 0.99858));
            coe.Add(new Coe("c4", 290, 0.998585));
            coe.Add(new Coe("c4", 291, 0.998589));
            coe.Add(new Coe("c4", 292, 0.998593));
            coe.Add(new Coe("c4", 293, 0.998598));
            coe.Add(new Coe("c4", 294, 0.998602));
            coe.Add(new Coe("c4", 295, 0.998606));
            coe.Add(new Coe("c4", 296, 0.998611));
            coe.Add(new Coe("c4", 297, 0.998615));
            coe.Add(new Coe("c4", 298, 0.998619));
            coe.Add(new Coe("c4", 299, 0.998623));
            coe.Add(new Coe("c4", 300, 0.998627));
            coe.Add(new Coe("c4", 301, 0.998632));
            coe.Add(new Coe("c4", 302, 0.998636));
            coe.Add(new Coe("c4", 303, 0.99864));
            coe.Add(new Coe("c4", 304, 0.998644));
            coe.Add(new Coe("c4", 305, 0.998648));
            coe.Add(new Coe("c4", 306, 0.998652));
            coe.Add(new Coe("c4", 307, 0.998656));
            coe.Add(new Coe("c4", 308, 0.99866));
            coe.Add(new Coe("c4", 309, 0.998664));
            coe.Add(new Coe("c4", 310, 0.998668));
            coe.Add(new Coe("c4", 311, 0.998671));
            coe.Add(new Coe("c4", 312, 0.998675));
            coe.Add(new Coe("c4", 313, 0.998679));
            coe.Add(new Coe("c4", 314, 0.998683));
            coe.Add(new Coe("c4", 315, 0.998687));
            coe.Add(new Coe("c4", 316, 0.99869));
            coe.Add(new Coe("c4", 317, 0.998694));
            coe.Add(new Coe("c4", 318, 0.998698));
            coe.Add(new Coe("c4", 319, 0.998701));
            coe.Add(new Coe("c4", 320, 0.998705));
            coe.Add(new Coe("c4", 321, 0.998709));
            coe.Add(new Coe("c4", 322, 0.998712));
            coe.Add(new Coe("c4", 323, 0.998716));
            coe.Add(new Coe("c4", 324, 0.99872));
            coe.Add(new Coe("c4", 325, 0.998723));
            coe.Add(new Coe("c4", 326, 0.998727));
            coe.Add(new Coe("c4", 327, 0.99873));
            coe.Add(new Coe("c4", 328, 0.998734));
            coe.Add(new Coe("c4", 329, 0.998737));
            coe.Add(new Coe("c4", 330, 0.99874));
            coe.Add(new Coe("c4", 331, 0.998744));
            coe.Add(new Coe("c4", 332, 0.998747));
            coe.Add(new Coe("c4", 333, 0.998751));
            coe.Add(new Coe("c4", 334, 0.998754));
            coe.Add(new Coe("c4", 335, 0.998757));
            coe.Add(new Coe("c4", 336, 0.998761));
            coe.Add(new Coe("c4", 337, 0.998764));
            coe.Add(new Coe("c4", 338, 0.998767));
            coe.Add(new Coe("c4", 339, 0.99877));
            coe.Add(new Coe("c4", 340, 0.998774));
            coe.Add(new Coe("c4", 341, 0.998777));
            coe.Add(new Coe("c4", 342, 0.99878));
            coe.Add(new Coe("c4", 343, 0.998783));
            coe.Add(new Coe("c4", 344, 0.998786));
            coe.Add(new Coe("c4", 345, 0.99879));
            coe.Add(new Coe("c4", 346, 0.998793));
            coe.Add(new Coe("c4", 347, 0.998796));
            coe.Add(new Coe("c4", 348, 0.998799));
            coe.Add(new Coe("c4", 349, 0.998802));
            coe.Add(new Coe("c4", 350, 0.998805));
            coe.Add(new Coe("c4", 351, 0.998808));
            coe.Add(new Coe("c4", 352, 0.998811));
            coe.Add(new Coe("c4", 353, 0.998814));
            coe.Add(new Coe("c4", 354, 0.998817));
            coe.Add(new Coe("c4", 355, 0.99882));
            coe.Add(new Coe("c4", 356, 0.998823));
            coe.Add(new Coe("c4", 357, 0.998826));
            coe.Add(new Coe("c4", 358, 0.998829));
            coe.Add(new Coe("c4", 359, 0.998832));
            coe.Add(new Coe("c4", 360, 0.998835));
            coe.Add(new Coe("c4", 361, 0.998837));
            coe.Add(new Coe("c4", 362, 0.99884));
            coe.Add(new Coe("c4", 363, 0.998843));
            coe.Add(new Coe("c4", 364, 0.998846));
            coe.Add(new Coe("c4", 365, 0.998849));
            coe.Add(new Coe("c4", 366, 0.998851));
            coe.Add(new Coe("c4", 367, 0.998854));
            coe.Add(new Coe("c4", 368, 0.998857));
            coe.Add(new Coe("c4", 369, 0.99886));
            coe.Add(new Coe("c4", 370, 0.998862));
            coe.Add(new Coe("c4", 371, 0.998865));
            coe.Add(new Coe("c4", 372, 0.998868));
            coe.Add(new Coe("c4", 373, 0.998871));
            coe.Add(new Coe("c4", 374, 0.998873));
            coe.Add(new Coe("c4", 375, 0.998876));
            coe.Add(new Coe("c4", 376, 0.998879));
            coe.Add(new Coe("c4", 377, 0.998881));
            coe.Add(new Coe("c4", 378, 0.998884));
            coe.Add(new Coe("c4", 379, 0.998886));
            coe.Add(new Coe("c4", 380, 0.998889));
            coe.Add(new Coe("c4", 381, 0.998892));
            coe.Add(new Coe("c4", 382, 0.998894));
            coe.Add(new Coe("c4", 383, 0.998897));
            coe.Add(new Coe("c4", 384, 0.998899));
            coe.Add(new Coe("c4", 385, 0.998902));
            coe.Add(new Coe("c4", 386, 0.998904));
            coe.Add(new Coe("c4", 387, 0.998907));
            coe.Add(new Coe("c4", 388, 0.998909));
            coe.Add(new Coe("c4", 389, 0.998912));
            coe.Add(new Coe("c4", 390, 0.998914));
            coe.Add(new Coe("c4", 391, 0.998917));
            coe.Add(new Coe("c4", 392, 0.998919));
            coe.Add(new Coe("c4", 393, 0.998921));
            coe.Add(new Coe("c4", 394, 0.998924));
            coe.Add(new Coe("c4", 395, 0.998926));
            coe.Add(new Coe("c4", 396, 0.998929));
            coe.Add(new Coe("c4", 397, 0.998931));
            coe.Add(new Coe("c4", 398, 0.998933));
            coe.Add(new Coe("c4", 399, 0.998936));
            coe.Add(new Coe("c4", 400, 0.998938));
            coe.Add(new Coe("c4", 401, 0.99894));
            coe.Add(new Coe("c4", 402, 0.998943));
            coe.Add(new Coe("c4", 403, 0.998945));
            coe.Add(new Coe("c4", 404, 0.998947));
            coe.Add(new Coe("c4", 405, 0.99895));
            coe.Add(new Coe("c4", 406, 0.998952));
            coe.Add(new Coe("c4", 407, 0.998954));
            coe.Add(new Coe("c4", 408, 0.998956));
            coe.Add(new Coe("c4", 409, 0.998959));
            coe.Add(new Coe("c4", 410, 0.998961));
            coe.Add(new Coe("c4", 411, 0.998963));
            coe.Add(new Coe("c4", 412, 0.998965));
            coe.Add(new Coe("c4", 413, 0.998967));
            coe.Add(new Coe("c4", 414, 0.99897));
            coe.Add(new Coe("c4", 415, 0.998972));
            coe.Add(new Coe("c4", 416, 0.998974));
            coe.Add(new Coe("c4", 417, 0.998976));
            coe.Add(new Coe("c4", 418, 0.998978));
            coe.Add(new Coe("c4", 419, 0.99898));
            coe.Add(new Coe("c4", 420, 0.998982));
            coe.Add(new Coe("c4", 421, 0.998985));
            coe.Add(new Coe("c4", 422, 0.998987));
            coe.Add(new Coe("c4", 423, 0.998989));
            coe.Add(new Coe("c4", 424, 0.998991));
            coe.Add(new Coe("c4", 425, 0.998993));
            coe.Add(new Coe("c4", 426, 0.998995));
            coe.Add(new Coe("c4", 427, 0.998997));
            coe.Add(new Coe("c4", 428, 0.998999));
            coe.Add(new Coe("c4", 429, 0.999001));
            coe.Add(new Coe("c4", 430, 0.999003));
            coe.Add(new Coe("c4", 431, 0.999005));
            coe.Add(new Coe("c4", 432, 0.999007));
            coe.Add(new Coe("c4", 433, 0.999009));
            coe.Add(new Coe("c4", 434, 0.999011));
            coe.Add(new Coe("c4", 435, 0.999013));
            coe.Add(new Coe("c4", 436, 0.999015));
            coe.Add(new Coe("c4", 437, 0.999017));
            coe.Add(new Coe("c4", 438, 0.999019));
            coe.Add(new Coe("c4", 439, 0.999021));
            coe.Add(new Coe("c4", 440, 0.999023));
            coe.Add(new Coe("c4", 441, 0.999025));
            coe.Add(new Coe("c4", 442, 0.999027));
            coe.Add(new Coe("c4", 443, 0.999028));
            coe.Add(new Coe("c4", 444, 0.99903));
            coe.Add(new Coe("c4", 445, 0.999032));
            coe.Add(new Coe("c4", 446, 0.999034));
            coe.Add(new Coe("c4", 447, 0.999036));
            coe.Add(new Coe("c4", 448, 0.999038));
            coe.Add(new Coe("c4", 449, 0.99904));
            coe.Add(new Coe("c4", 450, 0.999042));
            coe.Add(new Coe("c4", 451, 0.999043));
            coe.Add(new Coe("c4", 452, 0.999045));
            coe.Add(new Coe("c4", 453, 0.999047));
            coe.Add(new Coe("c4", 454, 0.999049));
            coe.Add(new Coe("c4", 455, 0.999051));
            coe.Add(new Coe("c4", 456, 0.999052));
            coe.Add(new Coe("c4", 457, 0.999054));
            coe.Add(new Coe("c4", 458, 0.999056));
            coe.Add(new Coe("c4", 459, 0.999058));
            coe.Add(new Coe("c4", 460, 0.99906));
            coe.Add(new Coe("c4", 461, 0.999061));
            coe.Add(new Coe("c4", 462, 0.999063));
            coe.Add(new Coe("c4", 463, 0.999065));
            coe.Add(new Coe("c4", 464, 0.999067));
            coe.Add(new Coe("c4", 465, 0.999068));
            coe.Add(new Coe("c4", 466, 0.99907));
            coe.Add(new Coe("c4", 467, 0.999072));
            coe.Add(new Coe("c4", 468, 0.999073));
            coe.Add(new Coe("c4", 469, 0.999075));
            coe.Add(new Coe("c4", 470, 0.999077));
            coe.Add(new Coe("c4", 471, 0.999078));
            coe.Add(new Coe("c4", 472, 0.99908));
            coe.Add(new Coe("c4", 473, 0.999082));
            coe.Add(new Coe("c4", 474, 0.999084));
            coe.Add(new Coe("c4", 475, 0.999085));
            coe.Add(new Coe("c4", 476, 0.999087));
            coe.Add(new Coe("c4", 477, 0.999088));
            coe.Add(new Coe("c4", 478, 0.99909));
            coe.Add(new Coe("c4", 479, 0.999092));
            coe.Add(new Coe("c4", 480, 0.999093));
            coe.Add(new Coe("c4", 481, 0.999095));
            coe.Add(new Coe("c4", 482, 0.999097));
            coe.Add(new Coe("c4", 483, 0.999098));
            coe.Add(new Coe("c4", 484, 0.9991));
            coe.Add(new Coe("c4", 485, 0.999101));
            coe.Add(new Coe("c4", 486, 0.999103));
            coe.Add(new Coe("c4", 487, 0.999104));
            coe.Add(new Coe("c4", 488, 0.999106));
            coe.Add(new Coe("c4", 489, 0.999108));
            coe.Add(new Coe("c4", 490, 0.999109));
            coe.Add(new Coe("c4", 491, 0.999111));
            coe.Add(new Coe("c4", 492, 0.999112));
            coe.Add(new Coe("c4", 493, 0.999114));
            coe.Add(new Coe("c4", 494, 0.999115));
            coe.Add(new Coe("c4", 495, 0.999117));
            coe.Add(new Coe("c4", 496, 0.999118));
            coe.Add(new Coe("c4", 497, 0.99912));
            coe.Add(new Coe("c4", 498, 0.999121));
            coe.Add(new Coe("c4", 499, 0.999123));
            coe.Add(new Coe("c4", 500, 0.999124));
            coe.Add(new Coe("c4", 501, 0.9995001));
            coe.Add(new Coe("c4", 502, 0.9995011));
            coe.Add(new Coe("c4", 503, 0.9995021));
            coe.Add(new Coe("c4", 504, 0.9995031));
            coe.Add(new Coe("c4", 505, 0.9995041));
            coe.Add(new Coe("c4", 506, 0.9995051));
            coe.Add(new Coe("c4", 507, 0.9995061));
            coe.Add(new Coe("c4", 508, 0.999507));
            coe.Add(new Coe("c4", 509, 0.999508));
            coe.Add(new Coe("c4", 510, 0.999509));
            coe.Add(new Coe("c4", 511, 0.9995099));
            coe.Add(new Coe("c4", 512, 0.9995109));
            coe.Add(new Coe("c4", 513, 0.9995118));
            coe.Add(new Coe("c4", 514, 0.9995128));
            coe.Add(new Coe("c4", 515, 0.9995137));
            coe.Add(new Coe("c4", 516, 0.9995147));
            coe.Add(new Coe("c4", 517, 0.9995156));
            coe.Add(new Coe("c4", 518, 0.9995165));
            coe.Add(new Coe("c4", 519, 0.9995175));
            coe.Add(new Coe("c4", 520, 0.9995184));
            coe.Add(new Coe("c4", 521, 0.9995193));
            coe.Add(new Coe("c4", 522, 0.9995202));
            coe.Add(new Coe("c4", 523, 0.9995212));
            coe.Add(new Coe("c4", 524, 0.9995221));
            coe.Add(new Coe("c4", 525, 0.999523));
            coe.Add(new Coe("c4", 526, 0.9995239));
            coe.Add(new Coe("c4", 527, 0.9995248));
            coe.Add(new Coe("c4", 528, 0.9995257));
            coe.Add(new Coe("c4", 529, 0.9995266));
            coe.Add(new Coe("c4", 530, 0.9995275));
            coe.Add(new Coe("c4", 531, 0.9995284));
            coe.Add(new Coe("c4", 532, 0.9995293));
            coe.Add(new Coe("c4", 533, 0.9995302));
            coe.Add(new Coe("c4", 534, 0.9995311));
            coe.Add(new Coe("c4", 535, 0.9995319));
            coe.Add(new Coe("c4", 536, 0.9995328));
            coe.Add(new Coe("c4", 537, 0.9995337));
            coe.Add(new Coe("c4", 538, 0.9995345));
            coe.Add(new Coe("c4", 539, 0.9995354));
            coe.Add(new Coe("c4", 540, 0.9995363));
            coe.Add(new Coe("c4", 541, 0.9995372));
            coe.Add(new Coe("c4", 542, 0.999538));
            coe.Add(new Coe("c4", 543, 0.9995388));
            coe.Add(new Coe("c4", 544, 0.9995397));
            coe.Add(new Coe("c4", 545, 0.9995406));
            coe.Add(new Coe("c4", 546, 0.9995414));
            coe.Add(new Coe("c4", 547, 0.9995422));
            coe.Add(new Coe("c4", 548, 0.9995431));
            coe.Add(new Coe("c4", 549, 0.9995439));
            coe.Add(new Coe("c4", 550, 0.9995447));
            coe.Add(new Coe("c4", 551, 0.9995456));
            coe.Add(new Coe("c4", 552, 0.9995464));
            coe.Add(new Coe("c4", 553, 0.9995472));
            coe.Add(new Coe("c4", 554, 0.999548));
            coe.Add(new Coe("c4", 555, 0.9995489));
            coe.Add(new Coe("c4", 556, 0.9995496));
            coe.Add(new Coe("c4", 557, 0.9995505));
            coe.Add(new Coe("c4", 558, 0.9995513));
            coe.Add(new Coe("c4", 559, 0.9995521));
            coe.Add(new Coe("c4", 560, 0.9995528));
            coe.Add(new Coe("c4", 561, 0.9995537));
            coe.Add(new Coe("c4", 562, 0.9995545));
            coe.Add(new Coe("c4", 563, 0.9995552));
            coe.Add(new Coe("c4", 564, 0.9995561));
            coe.Add(new Coe("c4", 565, 0.9995568));
            coe.Add(new Coe("c4", 566, 0.9995576));
            coe.Add(new Coe("c4", 567, 0.9995584));
            coe.Add(new Coe("c4", 568, 0.9995592));
            coe.Add(new Coe("c4", 569, 0.9995599));
            coe.Add(new Coe("c4", 570, 0.9995607));
            coe.Add(new Coe("c4", 571, 0.9995615));
            coe.Add(new Coe("c4", 572, 0.9995623));
            coe.Add(new Coe("c4", 573, 0.999563));
            coe.Add(new Coe("c4", 574, 0.9995638));
            coe.Add(new Coe("c4", 575, 0.9995645));
            coe.Add(new Coe("c4", 576, 0.9995653));
            coe.Add(new Coe("c4", 577, 0.9995661));
            coe.Add(new Coe("c4", 578, 0.9995668));
            coe.Add(new Coe("c4", 579, 0.9995676));
            coe.Add(new Coe("c4", 580, 0.9995683));
            coe.Add(new Coe("c4", 581, 0.9995691));
            coe.Add(new Coe("c4", 582, 0.9995698));
            coe.Add(new Coe("c4", 583, 0.9995705));
            coe.Add(new Coe("c4", 584, 0.9995713));
            coe.Add(new Coe("c4", 585, 0.999572));
            coe.Add(new Coe("c4", 586, 0.9995728));
            coe.Add(new Coe("c4", 587, 0.9995735));
            coe.Add(new Coe("c4", 588, 0.9995742));
            coe.Add(new Coe("c4", 589, 0.9995749));
            coe.Add(new Coe("c4", 590, 0.9995756));
            coe.Add(new Coe("c4", 591, 0.9995764));
            coe.Add(new Coe("c4", 592, 0.9995771));
            coe.Add(new Coe("c4", 593, 0.9995778));
            coe.Add(new Coe("c4", 594, 0.9995785));
            coe.Add(new Coe("c4", 595, 0.9995792));
            coe.Add(new Coe("c4", 596, 0.9995799));
            coe.Add(new Coe("c4", 597, 0.9995806));
            coe.Add(new Coe("c4", 598, 0.9995813));
            coe.Add(new Coe("c4", 599, 0.9995821));
            coe.Add(new Coe("c4", 600, 0.9995827));
            coe.Add(new Coe("c4", 601, 0.9995834));
            coe.Add(new Coe("c4", 602, 0.9995841));
            coe.Add(new Coe("c4", 603, 0.9995848));
            coe.Add(new Coe("c4", 604, 0.9995855));
            coe.Add(new Coe("c4", 605, 0.9995862));
            coe.Add(new Coe("c4", 606, 0.9995869));
            coe.Add(new Coe("c4", 607, 0.9995875));
            coe.Add(new Coe("c4", 608, 0.9995883));
            coe.Add(new Coe("c4", 609, 0.9995889));
            coe.Add(new Coe("c4", 610, 0.9995896));
            coe.Add(new Coe("c4", 611, 0.9995903));
            coe.Add(new Coe("c4", 612, 0.9995909));
            coe.Add(new Coe("c4", 613, 0.9995916));
            coe.Add(new Coe("c4", 614, 0.9995922));
            coe.Add(new Coe("c4", 615, 0.9995929));
            coe.Add(new Coe("c4", 616, 0.9995936));
            coe.Add(new Coe("c4", 617, 0.9995942));
            coe.Add(new Coe("c4", 618, 0.9995949));
            coe.Add(new Coe("c4", 619, 0.9995955));
            coe.Add(new Coe("c4", 620, 0.9995962));
            coe.Add(new Coe("c4", 621, 0.9995968));
            coe.Add(new Coe("c4", 622, 0.9995975));
            coe.Add(new Coe("c4", 623, 0.9995981));
            coe.Add(new Coe("c4", 624, 0.9995988));
            coe.Add(new Coe("c4", 625, 0.9995995));
            coe.Add(new Coe("c4", 626, 0.9996001));
            coe.Add(new Coe("c4", 627, 0.9996007));
            coe.Add(new Coe("c4", 628, 0.9996014));
            coe.Add(new Coe("c4", 629, 0.999602));
            coe.Add(new Coe("c4", 630, 0.9996026));
            coe.Add(new Coe("c4", 631, 0.9996033));
            coe.Add(new Coe("c4", 632, 0.9996039));
            coe.Add(new Coe("c4", 633, 0.9996045));
            coe.Add(new Coe("c4", 634, 0.9996051));
            coe.Add(new Coe("c4", 635, 0.9996058));
            coe.Add(new Coe("c4", 636, 0.9996064));
            coe.Add(new Coe("c4", 637, 0.999607));
            coe.Add(new Coe("c4", 638, 0.9996076));
            coe.Add(new Coe("c4", 639, 0.9996082));
            coe.Add(new Coe("c4", 640, 0.9996088));
            coe.Add(new Coe("c4", 641, 0.9996095));
            coe.Add(new Coe("c4", 642, 0.9996101));
            coe.Add(new Coe("c4", 643, 0.9996107));
            coe.Add(new Coe("c4", 644, 0.9996113));
            coe.Add(new Coe("c4", 645, 0.9996119));
            coe.Add(new Coe("c4", 646, 0.9996125));
            coe.Add(new Coe("c4", 647, 0.9996131));
            coe.Add(new Coe("c4", 648, 0.9996137));
            coe.Add(new Coe("c4", 649, 0.9996143));
            coe.Add(new Coe("c4", 650, 0.9996149));
            coe.Add(new Coe("c4", 651, 0.9996154));
            coe.Add(new Coe("c4", 652, 0.999616));
            coe.Add(new Coe("c4", 653, 0.9996166));
            coe.Add(new Coe("c4", 654, 0.9996172));
            coe.Add(new Coe("c4", 655, 0.9996178));
            coe.Add(new Coe("c4", 656, 0.9996184));
            coe.Add(new Coe("c4", 657, 0.9996189));
            coe.Add(new Coe("c4", 658, 0.9996195));
            coe.Add(new Coe("c4", 659, 0.9996201));
            coe.Add(new Coe("c4", 660, 0.9996207));
            coe.Add(new Coe("c4", 661, 0.9996213));
            coe.Add(new Coe("c4", 662, 0.9996219));
            coe.Add(new Coe("c4", 663, 0.9996224));
            coe.Add(new Coe("c4", 664, 0.999623));
            coe.Add(new Coe("c4", 665, 0.9996235));
            coe.Add(new Coe("c4", 666, 0.9996241));
            coe.Add(new Coe("c4", 667, 0.9996247));
            coe.Add(new Coe("c4", 668, 0.9996253));
            coe.Add(new Coe("c4", 669, 0.9996258));
            coe.Add(new Coe("c4", 670, 0.9996264));
            coe.Add(new Coe("c4", 671, 0.9996269));
            coe.Add(new Coe("c4", 672, 0.9996275));
            coe.Add(new Coe("c4", 673, 0.9996281));
            coe.Add(new Coe("c4", 674, 0.9996286));
            coe.Add(new Coe("c4", 675, 0.9996291));
            coe.Add(new Coe("c4", 676, 0.9996297));
            coe.Add(new Coe("c4", 677, 0.9996303));
            coe.Add(new Coe("c4", 678, 0.9996308));
            coe.Add(new Coe("c4", 679, 0.9996313));
            coe.Add(new Coe("c4", 680, 0.9996319));
            coe.Add(new Coe("c4", 681, 0.9996324));
            coe.Add(new Coe("c4", 682, 0.999633));
            coe.Add(new Coe("c4", 683, 0.9996335));
            coe.Add(new Coe("c4", 684, 0.999634));
            coe.Add(new Coe("c4", 685, 0.9996346));
            coe.Add(new Coe("c4", 686, 0.9996351));
            coe.Add(new Coe("c4", 687, 0.9996356));
            coe.Add(new Coe("c4", 688, 0.9996362));
            coe.Add(new Coe("c4", 689, 0.9996367));
            coe.Add(new Coe("c4", 690, 0.9996372));
            coe.Add(new Coe("c4", 691, 0.9996377));
            coe.Add(new Coe("c4", 692, 0.9996383));
            coe.Add(new Coe("c4", 693, 0.9996388));
            coe.Add(new Coe("c4", 694, 0.9996393));
            coe.Add(new Coe("c4", 695, 0.9996398));
            coe.Add(new Coe("c4", 696, 0.9996403));
            coe.Add(new Coe("c4", 697, 0.9996409));
            coe.Add(new Coe("c4", 698, 0.9996414));
            coe.Add(new Coe("c4", 699, 0.9996419));
            coe.Add(new Coe("c4", 700, 0.9996424));
            coe.Add(new Coe("c4", 701, 0.9996429));
            coe.Add(new Coe("c4", 702, 0.9996434));
            coe.Add(new Coe("c4", 703, 0.9996439));
            coe.Add(new Coe("c4", 704, 0.9996445));
            coe.Add(new Coe("c4", 705, 0.9996449));
            coe.Add(new Coe("c4", 706, 0.9996455));
            coe.Add(new Coe("c4", 707, 0.9996459));
            coe.Add(new Coe("c4", 708, 0.9996465));
            coe.Add(new Coe("c4", 709, 0.999647));
            coe.Add(new Coe("c4", 710, 0.9996474));
            coe.Add(new Coe("c4", 711, 0.999648));
            coe.Add(new Coe("c4", 712, 0.9996485));
            coe.Add(new Coe("c4", 713, 0.9996489));
            coe.Add(new Coe("c4", 714, 0.9996494));
            coe.Add(new Coe("c4", 715, 0.9996499));
            coe.Add(new Coe("c4", 716, 0.9996504));
            coe.Add(new Coe("c4", 717, 0.9996509));
            coe.Add(new Coe("c4", 718, 0.9996514));
            coe.Add(new Coe("c4", 719, 0.9996518));
            coe.Add(new Coe("c4", 720, 0.9996523));
            coe.Add(new Coe("c4", 721, 0.9996529));
            coe.Add(new Coe("c4", 722, 0.9996533));
            coe.Add(new Coe("c4", 723, 0.9996538));
            coe.Add(new Coe("c4", 724, 0.9996543));
            coe.Add(new Coe("c4", 725, 0.9996548));
            coe.Add(new Coe("c4", 726, 0.9996552));
            coe.Add(new Coe("c4", 727, 0.9996557));
            coe.Add(new Coe("c4", 728, 0.9996562));
            coe.Add(new Coe("c4", 729, 0.9996567));
            coe.Add(new Coe("c4", 730, 0.9996571));
            coe.Add(new Coe("c4", 731, 0.9996576));
            coe.Add(new Coe("c4", 732, 0.999658));
            coe.Add(new Coe("c4", 733, 0.9996585));
            coe.Add(new Coe("c4", 734, 0.999659));
            coe.Add(new Coe("c4", 735, 0.9996595));
            coe.Add(new Coe("c4", 736, 0.9996599));
            coe.Add(new Coe("c4", 737, 0.9996604));
            coe.Add(new Coe("c4", 738, 0.9996608));
            coe.Add(new Coe("c4", 739, 0.9996613));
            coe.Add(new Coe("c4", 740, 0.9996617));
            coe.Add(new Coe("c4", 741, 0.9996622));
            coe.Add(new Coe("c4", 742, 0.9996627));
            coe.Add(new Coe("c4", 743, 0.9996631));
            coe.Add(new Coe("c4", 744, 0.9996636));
            coe.Add(new Coe("c4", 745, 0.999664));
            coe.Add(new Coe("c4", 746, 0.9996645));
            coe.Add(new Coe("c4", 747, 0.999665));
            coe.Add(new Coe("c4", 748, 0.9996654));
            coe.Add(new Coe("c4", 749, 0.9996659));
            coe.Add(new Coe("c4", 750, 0.9996663));
            coe.Add(new Coe("c4", 751, 0.9996668));
            coe.Add(new Coe("c4", 752, 0.9996672));
            coe.Add(new Coe("c4", 753, 0.9996676));
            coe.Add(new Coe("c4", 754, 0.9996681));
            coe.Add(new Coe("c4", 755, 0.9996685));
            coe.Add(new Coe("c4", 756, 0.999669));
            coe.Add(new Coe("c4", 757, 0.9996694));
            coe.Add(new Coe("c4", 758, 0.9996698));
            coe.Add(new Coe("c4", 759, 0.9996703));
            coe.Add(new Coe("c4", 760, 0.9996707));
            coe.Add(new Coe("c4", 761, 0.9996711));
            coe.Add(new Coe("c4", 762, 0.9996715));
            coe.Add(new Coe("c4", 763, 0.999672));
            coe.Add(new Coe("c4", 764, 0.9996724));
            coe.Add(new Coe("c4", 765, 0.9996728));
            coe.Add(new Coe("c4", 766, 0.9996732));
            coe.Add(new Coe("c4", 767, 0.9996737));
            coe.Add(new Coe("c4", 768, 0.9996741));
            coe.Add(new Coe("c4", 769, 0.9996746));
            coe.Add(new Coe("c4", 770, 0.999675));
            coe.Add(new Coe("c4", 771, 0.9996754));
            coe.Add(new Coe("c4", 772, 0.9996758));
            coe.Add(new Coe("c4", 773, 0.9996762));
            coe.Add(new Coe("c4", 774, 0.9996766));
            coe.Add(new Coe("c4", 775, 0.9996771));
            coe.Add(new Coe("c4", 776, 0.9996775));
            coe.Add(new Coe("c4", 777, 0.9996779));
            coe.Add(new Coe("c4", 778, 0.9996783));
            coe.Add(new Coe("c4", 779, 0.9996787));
            coe.Add(new Coe("c4", 780, 0.9996791));
            coe.Add(new Coe("c4", 781, 0.9996796));
            coe.Add(new Coe("c4", 782, 0.9996799));
            coe.Add(new Coe("c4", 783, 0.9996803));
            coe.Add(new Coe("c4", 784, 0.9996808));
            coe.Add(new Coe("c4", 785, 0.9996812));
            coe.Add(new Coe("c4", 786, 0.9996816));
            coe.Add(new Coe("c4", 787, 0.999682));
            coe.Add(new Coe("c4", 788, 0.9996824));
            coe.Add(new Coe("c4", 789, 0.9996828));
            coe.Add(new Coe("c4", 790, 0.9996832));
            coe.Add(new Coe("c4", 791, 0.9996836));
            coe.Add(new Coe("c4", 792, 0.999684));
            coe.Add(new Coe("c4", 793, 0.9996844));
            coe.Add(new Coe("c4", 794, 0.9996848));
            coe.Add(new Coe("c4", 795, 0.9996852));
            coe.Add(new Coe("c4", 796, 0.9996856));
            coe.Add(new Coe("c4", 797, 0.999686));
            coe.Add(new Coe("c4", 798, 0.9996864));
            coe.Add(new Coe("c4", 799, 0.9996868));
            coe.Add(new Coe("c4", 800, 0.9996871));
            coe.Add(new Coe("c4", 801, 0.9996876));
            coe.Add(new Coe("c4", 802, 0.9996879));
            coe.Add(new Coe("c4", 803, 0.9996883));
            coe.Add(new Coe("c4", 804, 0.9996887));
            coe.Add(new Coe("c4", 805, 0.9996891));
            coe.Add(new Coe("c4", 806, 0.9996895));
            coe.Add(new Coe("c4", 807, 0.9996899));
            coe.Add(new Coe("c4", 808, 0.9996902));
            coe.Add(new Coe("c4", 809, 0.9996907));
            coe.Add(new Coe("c4", 810, 0.999691));
            coe.Add(new Coe("c4", 811, 0.9996914));
            coe.Add(new Coe("c4", 812, 0.9996918));
            coe.Add(new Coe("c4", 813, 0.9996921));
            coe.Add(new Coe("c4", 814, 0.9996926));
            coe.Add(new Coe("c4", 815, 0.9996929));
            coe.Add(new Coe("c4", 816, 0.9996933));
            coe.Add(new Coe("c4", 817, 0.9996937));
            coe.Add(new Coe("c4", 818, 0.999694));
            coe.Add(new Coe("c4", 819, 0.9996944));
            coe.Add(new Coe("c4", 820, 0.9996948));
            coe.Add(new Coe("c4", 821, 0.9996952));
            coe.Add(new Coe("c4", 822, 0.9996955));
            coe.Add(new Coe("c4", 823, 0.9996959));
            coe.Add(new Coe("c4", 824, 0.9996963));
            coe.Add(new Coe("c4", 825, 0.9996967));
            coe.Add(new Coe("c4", 826, 0.999697));
            coe.Add(new Coe("c4", 827, 0.9996974));
            coe.Add(new Coe("c4", 828, 0.9996977));
            coe.Add(new Coe("c4", 829, 0.9996981));
            coe.Add(new Coe("c4", 830, 0.9996985));
            coe.Add(new Coe("c4", 831, 0.9996988));
            coe.Add(new Coe("c4", 832, 0.9996992));
            coe.Add(new Coe("c4", 833, 0.9996996));
            coe.Add(new Coe("c4", 834, 0.9997));
            coe.Add(new Coe("c4", 835, 0.9997003));
            coe.Add(new Coe("c4", 836, 0.9997007));
            coe.Add(new Coe("c4", 837, 0.999701));
            coe.Add(new Coe("c4", 838, 0.9997014));
            coe.Add(new Coe("c4", 839, 0.9997017));
            coe.Add(new Coe("c4", 840, 0.9997021));
            coe.Add(new Coe("c4", 841, 0.9997025));
            coe.Add(new Coe("c4", 842, 0.9997028));
            coe.Add(new Coe("c4", 843, 0.9997031));
            coe.Add(new Coe("c4", 844, 0.9997035));
            coe.Add(new Coe("c4", 845, 0.9997038));
            coe.Add(new Coe("c4", 846, 0.9997042));
            coe.Add(new Coe("c4", 847, 0.9997045));
            coe.Add(new Coe("c4", 848, 0.9997049));
            coe.Add(new Coe("c4", 849, 0.9997053));
            coe.Add(new Coe("c4", 850, 0.9997056));
            coe.Add(new Coe("c4", 851, 0.9997059));
            coe.Add(new Coe("c4", 852, 0.9997063));
            coe.Add(new Coe("c4", 853, 0.9997066));
            coe.Add(new Coe("c4", 854, 0.999707));
            coe.Add(new Coe("c4", 855, 0.9997073));
            coe.Add(new Coe("c4", 856, 0.9997076));
            coe.Add(new Coe("c4", 857, 0.999708));
            coe.Add(new Coe("c4", 858, 0.9997084));
            coe.Add(new Coe("c4", 859, 0.9997087));
            coe.Add(new Coe("c4", 860, 0.999709));
            coe.Add(new Coe("c4", 861, 0.9997094));
            coe.Add(new Coe("c4", 862, 0.9997097));
            coe.Add(new Coe("c4", 863, 0.99971));
            coe.Add(new Coe("c4", 864, 0.9997104));
            coe.Add(new Coe("c4", 865, 0.9997107));
            coe.Add(new Coe("c4", 866, 0.999711));
            coe.Add(new Coe("c4", 867, 0.9997113));
            coe.Add(new Coe("c4", 868, 0.9997117));
            coe.Add(new Coe("c4", 869, 0.999712));
            coe.Add(new Coe("c4", 870, 0.9997123));
            coe.Add(new Coe("c4", 871, 0.9997127));
            coe.Add(new Coe("c4", 872, 0.999713));
            coe.Add(new Coe("c4", 873, 0.9997134));
            coe.Add(new Coe("c4", 874, 0.9997137));
            coe.Add(new Coe("c4", 875, 0.999714));
            coe.Add(new Coe("c4", 876, 0.9997143));
            coe.Add(new Coe("c4", 877, 0.9997147));
            coe.Add(new Coe("c4", 878, 0.999715));
            coe.Add(new Coe("c4", 879, 0.9997153));
            coe.Add(new Coe("c4", 880, 0.9997156));
            coe.Add(new Coe("c4", 881, 0.9997159));
            coe.Add(new Coe("c4", 882, 0.9997163));
            coe.Add(new Coe("c4", 883, 0.9997166));
            coe.Add(new Coe("c4", 884, 0.9997169));
            coe.Add(new Coe("c4", 885, 0.9997172));
            coe.Add(new Coe("c4", 886, 0.9997175));
            coe.Add(new Coe("c4", 887, 0.9997179));
            coe.Add(new Coe("c4", 888, 0.9997182));
            coe.Add(new Coe("c4", 889, 0.9997185));
            coe.Add(new Coe("c4", 890, 0.9997188));
            coe.Add(new Coe("c4", 891, 0.9997191));
            coe.Add(new Coe("c4", 892, 0.9997194));
            coe.Add(new Coe("c4", 893, 0.9997198));
            coe.Add(new Coe("c4", 894, 0.9997201));
            coe.Add(new Coe("c4", 895, 0.9997204));
            coe.Add(new Coe("c4", 896, 0.9997207));
            coe.Add(new Coe("c4", 897, 0.9997211));
            coe.Add(new Coe("c4", 898, 0.9997213));
            coe.Add(new Coe("c4", 899, 0.9997216));
            coe.Add(new Coe("c4", 900, 0.9997219));
            coe.Add(new Coe("c4", 901, 0.9997222));
            coe.Add(new Coe("c4", 902, 0.9997225));
            coe.Add(new Coe("c4", 903, 0.9997229));
            coe.Add(new Coe("c4", 904, 0.9997232));
            coe.Add(new Coe("c4", 905, 0.9997235));
            coe.Add(new Coe("c4", 906, 0.9997238));
            coe.Add(new Coe("c4", 907, 0.9997241));
            coe.Add(new Coe("c4", 908, 0.9997244));
            coe.Add(new Coe("c4", 909, 0.9997247));
            coe.Add(new Coe("c4", 910, 0.999725));
            coe.Add(new Coe("c4", 911, 0.9997253));
            coe.Add(new Coe("c4", 912, 0.9997256));
            coe.Add(new Coe("c4", 913, 0.9997259));
            coe.Add(new Coe("c4", 914, 0.9997262));
            coe.Add(new Coe("c4", 915, 0.9997265));
            coe.Add(new Coe("c4", 916, 0.9997268));
            coe.Add(new Coe("c4", 917, 0.9997271));
            coe.Add(new Coe("c4", 918, 0.9997274));
            coe.Add(new Coe("c4", 919, 0.9997277));
            coe.Add(new Coe("c4", 920, 0.999728));
            coe.Add(new Coe("c4", 921, 0.9997283));
            coe.Add(new Coe("c4", 922, 0.9997286));
            coe.Add(new Coe("c4", 923, 0.9997289));
            coe.Add(new Coe("c4", 924, 0.9997292));
            coe.Add(new Coe("c4", 925, 0.9997295));
            coe.Add(new Coe("c4", 926, 0.9997298));
            coe.Add(new Coe("c4", 927, 0.9997301));
            coe.Add(new Coe("c4", 928, 0.9997303));
            coe.Add(new Coe("c4", 929, 0.9997306));
            coe.Add(new Coe("c4", 930, 0.9997309));
            coe.Add(new Coe("c4", 931, 0.9997312));
            coe.Add(new Coe("c4", 932, 0.9997315));
            coe.Add(new Coe("c4", 933, 0.9997318));
            coe.Add(new Coe("c4", 934, 0.9997321));
            coe.Add(new Coe("c4", 935, 0.9997324));
            coe.Add(new Coe("c4", 936, 0.9997327));
            coe.Add(new Coe("c4", 937, 0.999733));
            coe.Add(new Coe("c4", 938, 0.9997332));
            coe.Add(new Coe("c4", 939, 0.9997335));
            coe.Add(new Coe("c4", 940, 0.9997338));
            coe.Add(new Coe("c4", 941, 0.9997341));
            coe.Add(new Coe("c4", 942, 0.9997343));
            coe.Add(new Coe("c4", 943, 0.9997346));
            coe.Add(new Coe("c4", 944, 0.9997349));
            coe.Add(new Coe("c4", 945, 0.9997352));
            coe.Add(new Coe("c4", 946, 0.9997355));
            coe.Add(new Coe("c4", 947, 0.9997358));
            coe.Add(new Coe("c4", 948, 0.9997361));
            coe.Add(new Coe("c4", 949, 0.9997363));
            coe.Add(new Coe("c4", 950, 0.9997366));
            coe.Add(new Coe("c4", 951, 0.9997369));
            coe.Add(new Coe("c4", 952, 0.9997371));
            coe.Add(new Coe("c4", 953, 0.9997374));
            coe.Add(new Coe("c4", 954, 0.9997377));
            coe.Add(new Coe("c4", 955, 0.999738));
            coe.Add(new Coe("c4", 956, 0.9997383));
            coe.Add(new Coe("c4", 957, 0.9997385));
            coe.Add(new Coe("c4", 958, 0.9997388));
            coe.Add(new Coe("c4", 959, 0.9997391));
            coe.Add(new Coe("c4", 960, 0.9997393));
            coe.Add(new Coe("c4", 961, 0.9997396));
            coe.Add(new Coe("c4", 962, 0.9997399));
            coe.Add(new Coe("c4", 963, 0.9997402));
            coe.Add(new Coe("c4", 964, 0.9997404));
            coe.Add(new Coe("c4", 965, 0.9997407));
            coe.Add(new Coe("c4", 966, 0.999741));
            coe.Add(new Coe("c4", 967, 0.9997413));
            coe.Add(new Coe("c4", 968, 0.9997415));
            coe.Add(new Coe("c4", 969, 0.9997418));
            coe.Add(new Coe("c4", 970, 0.999742));
            coe.Add(new Coe("c4", 971, 0.9997423));
            coe.Add(new Coe("c4", 972, 0.9997426));
            coe.Add(new Coe("c4", 973, 0.9997428));
            coe.Add(new Coe("c4", 974, 0.9997431));
            coe.Add(new Coe("c4", 975, 0.9997433));
            coe.Add(new Coe("c4", 976, 0.9997436));
            coe.Add(new Coe("c4", 977, 0.9997439));
            coe.Add(new Coe("c4", 978, 0.9997441));
            coe.Add(new Coe("c4", 979, 0.9997444));
            coe.Add(new Coe("c4", 980, 0.9997447));
            coe.Add(new Coe("c4", 981, 0.999745));
            coe.Add(new Coe("c4", 982, 0.9997452));
            coe.Add(new Coe("c4", 983, 0.9997454));
            coe.Add(new Coe("c4", 984, 0.9997457));
            coe.Add(new Coe("c4", 985, 0.999746));
            coe.Add(new Coe("c4", 986, 0.9997462));
            coe.Add(new Coe("c4", 987, 0.9997465));
            coe.Add(new Coe("c4", 988, 0.9997467));
            coe.Add(new Coe("c4", 989, 0.999747));
            coe.Add(new Coe("c4", 990, 0.9997473));
            coe.Add(new Coe("c4", 991, 0.9997475));
            coe.Add(new Coe("c4", 992, 0.9997478));
            coe.Add(new Coe("c4", 993, 0.999748));
            coe.Add(new Coe("c4", 994, 0.9997483));
            coe.Add(new Coe("c4", 995, 0.9997485));
            coe.Add(new Coe("c4", 996, 0.9997488));
            coe.Add(new Coe("c4", 997, 0.999749));
            coe.Add(new Coe("c4", 998, 0.9997493));
            coe.Add(new Coe("c4", 999, 0.9997495));
            coe.Add(new Coe("c4", 1000, 0.9997498));
            coe.Add(new Coe("c4", 1001, 0.99975));
        }

        /// <summary> 
        /// d2
        /// </summary>
        public static void Coeinitial_d2()
        {
            if (coed2 != null && coed2.Count > 0)
            {
                return;
            }

            coed2.Add(new Coe("d2", 2, 1.128));
            coed2.Add(new Coe("d2", 3, 1.693));
            coed2.Add(new Coe("d2", 4, 2.059));
            coed2.Add(new Coe("d2", 5, 2.326));
            coed2.Add(new Coe("d2", 6, 2.534));
            coed2.Add(new Coe("d2", 7, 2.704));
            coed2.Add(new Coe("d2", 8, 2.847));
            coed2.Add(new Coe("d2", 9, 2.97));
            coed2.Add(new Coe("d2", 10, 3.078));
            coed2.Add(new Coe("d2", 11, 3.173));
            coed2.Add(new Coe("d2", 12, 3.258));
            coed2.Add(new Coe("d2", 13, 3.336));
            coed2.Add(new Coe("d2", 14, 3.407));
            coed2.Add(new Coe("d2", 15, 3.472));
            coed2.Add(new Coe("d2", 16, 3.532));
            coed2.Add(new Coe("d2", 17, 3.588));
            coed2.Add(new Coe("d2", 18, 3.64));
            coed2.Add(new Coe("d2", 19, 3.689));
            coed2.Add(new Coe("d2", 20, 3.735));
            coed2.Add(new Coe("d2", 21, 3.778));
            coed2.Add(new Coe("d2", 22, 3.819));
            coed2.Add(new Coe("d2", 23, 3.858));
            coed2.Add(new Coe("d2", 24, 3.895));
            coed2.Add(new Coe("d2", 25, 3.931));
            coed2.Add(new Coe("d2", 26, 3.964));
            coed2.Add(new Coe("d2", 27, 3.997));
            coed2.Add(new Coe("d2", 28, 4.027));
            coed2.Add(new Coe("d2", 29, 4.057));
            coed2.Add(new Coe("d2", 30, 4.086));
            coed2.Add(new Coe("d2", 31, 4.113));
            coed2.Add(new Coe("d2", 32, 4.139));
            coed2.Add(new Coe("d2", 33, 4.165));
            coed2.Add(new Coe("d2", 34, 4.189));
            coed2.Add(new Coe("d2", 35, 4.213));
            coed2.Add(new Coe("d2", 36, 4.236));
            coed2.Add(new Coe("d2", 37, 4.259));
            coed2.Add(new Coe("d2", 38, 4.28));
            coed2.Add(new Coe("d2", 39, 4.301));
            coed2.Add(new Coe("d2", 40, 4.322));
            coed2.Add(new Coe("d2", 41, 4.341));
            coed2.Add(new Coe("d2", 42, 4.361));
            coed2.Add(new Coe("d2", 43, 4.379));
            coed2.Add(new Coe("d2", 44, 4.398));
            coed2.Add(new Coe("d2", 45, 4.415));
            coed2.Add(new Coe("d2", 46, 4.433));
            coed2.Add(new Coe("d2", 47, 4.45));
            coed2.Add(new Coe("d2", 48, 4.466));
            coed2.Add(new Coe("d2", 49, 4.482));
            coed2.Add(new Coe("d2", 50, 4.498));
        }

        public static void Coeinitial_d3()
        {
            if (coed3 != null && coed3.Count > 0)
            {
                return;
            }

            coed3.Add(new Coe("d3", 2, 0.8525));
            coed3.Add(new Coe("d3", 3, 0.8884));
            coed3.Add(new Coe("d3", 4, 0.8798));
            coed3.Add(new Coe("d3", 5, 0.8641));
            coed3.Add(new Coe("d3", 6, 0.848));
            coed3.Add(new Coe("d3", 7, 0.8332));
            coed3.Add(new Coe("d3", 8, 0.8198));
            coed3.Add(new Coe("d3", 9, 0.8078));
            coed3.Add(new Coe("d3", 10, 0.7971));
            coed3.Add(new Coe("d3", 11, 0.7873));
            coed3.Add(new Coe("d3", 12, 0.7785));
            coed3.Add(new Coe("d3", 13, 0.7704));
            coed3.Add(new Coe("d3", 14, 0.763));
            coed3.Add(new Coe("d3", 15, 0.7562));
            coed3.Add(new Coe("d3", 16, 0.7499));
            coed3.Add(new Coe("d3", 17, 0.7441));
            coed3.Add(new Coe("d3", 18, 0.7386));
            coed3.Add(new Coe("d3", 19, 0.7335));
            coed3.Add(new Coe("d3", 20, 0.7287));
            coed3.Add(new Coe("d3", 21, 0.7242));
            coed3.Add(new Coe("d3", 22, 0.7199));
            coed3.Add(new Coe("d3", 23, 0.7159));
            coed3.Add(new Coe("d3", 24, 0.7121));
            coed3.Add(new Coe("d3", 25, 0.7084));
        }

        public static void Coeinitial_D3()
        {
            if (coeD3 != null && coeD3.Count > 0)
            {
                return;
            }

            coeD3.Add(new Coe("D3", 2, 0));
            coeD3.Add(new Coe("D3", 3, 0));
            coeD3.Add(new Coe("D3", 4, 0));
            coeD3.Add(new Coe("D3", 5, 0));
            coeD3.Add(new Coe("D3", 6, 0));
            coeD3.Add(new Coe("D3", 7, 0.076));
            coeD3.Add(new Coe("D3", 8, 0.136));
            coeD3.Add(new Coe("D3", 9, 0.184));
            coeD3.Add(new Coe("D3", 10, 0.223));
            coeD3.Add(new Coe("D3", 11, 0.256));
            coeD3.Add(new Coe("D3", 12, 0.283));
            coeD3.Add(new Coe("D3", 13, 0.307));
            coeD3.Add(new Coe("D3", 14, 0.328));
            coeD3.Add(new Coe("D3", 15, 0.347));
            coeD3.Add(new Coe("D3", 16, 0.363));
            coeD3.Add(new Coe("D3", 17, 0.378));
            coeD3.Add(new Coe("D3", 18, 0.391));
            coeD3.Add(new Coe("D3", 19, 0.403));
            coeD3.Add(new Coe("D3", 20, 0.415));
            coeD3.Add(new Coe("D3", 21, 0.425));
            coeD3.Add(new Coe("D3", 22, 0.434));
            coeD3.Add(new Coe("D3", 23, 0.443));
            coeD3.Add(new Coe("D3", 24, 0.451));
            coeD3.Add(new Coe("D3", 25, 0.459));
        }

        public static void Coeinitial_d4()
        {
            if (coed4 != null && coed4.Count > 0)
            {
                return;
            }

            coed4.Add(new Coe("d4", 02, 0.954));
            coed4.Add(new Coe("d4", 03, 1.588));
            coed4.Add(new Coe("d4", 04, 1.978));
            coed4.Add(new Coe("d4", 05, 2.257));
            coed4.Add(new Coe("d4", 06, 2.472));
            coed4.Add(new Coe("d4", 07, 2.645));
            coed4.Add(new Coe("d4", 08, 2.791));
            coed4.Add(new Coe("d4", 09, 2.915));
            coed4.Add(new Coe("d4", 10, 3.024));
            coed4.Add(new Coe("d4", 11, 3.121));
            coed4.Add(new Coe("d4", 12, 3.207));
            coed4.Add(new Coe("d4", 13, 3.285));
            coed4.Add(new Coe("d4", 14, 3.356));
            coed4.Add(new Coe("d4", 15, 3.422));
            coed4.Add(new Coe("d4", 16, 3.482));
            coed4.Add(new Coe("d4", 17, 3.538));
            coed4.Add(new Coe("d4", 18, 3.591));
            coed4.Add(new Coe("d4", 19, 3.64));
            coed4.Add(new Coe("d4", 20, 3.686));
            coed4.Add(new Coe("d4", 21, 3.73));
            coed4.Add(new Coe("d4", 22, 3.771));
            coed4.Add(new Coe("d4", 23, 3.811));
            coed4.Add(new Coe("d4", 24, 3.847));
            coed4.Add(new Coe("d4", 25, 3.883));
        }

        public static void Coeinitial_D4()
        {
            if (coeD4 != null && coeD4.Count > 0)
            {
                return;
            }

            coeD4.Add(new Coe("D4", 02, 3.267));
            coeD4.Add(new Coe("D4", 03, 2.574));
            coeD4.Add(new Coe("D4", 04, 2.282));
            coeD4.Add(new Coe("D4", 05, 2.114));
            coeD4.Add(new Coe("D4", 06, 2.004));
            coeD4.Add(new Coe("D4", 07, 1.924));
            coeD4.Add(new Coe("D4", 08, 1.864));
            coeD4.Add(new Coe("D4", 09, 1.816));
            coeD4.Add(new Coe("D4", 10, 1.777));
            coeD4.Add(new Coe("D4", 11, 1.744));
            coeD4.Add(new Coe("D4", 12, 1.717));
            coeD4.Add(new Coe("D4", 13, 1.693));
            coeD4.Add(new Coe("D4", 14, 1.672));
            coeD4.Add(new Coe("D4", 15, 1.653));
            coeD4.Add(new Coe("D4", 16, 1.637));
            coeD4.Add(new Coe("D4", 17, 1.622));
            coeD4.Add(new Coe("D4", 18, 1.608));
            coeD4.Add(new Coe("D4", 19, 1.597));
            coeD4.Add(new Coe("D4", 20, 1.585));
            coeD4.Add(new Coe("D4", 21, 1.575));
            coeD4.Add(new Coe("D4", 22, 1.566));
            coeD4.Add(new Coe("D4", 23, 1.557));
            coeD4.Add(new Coe("D4", 24, 1.548));
            coeD4.Add(new Coe("D4", 25, 1.541));
        }

        public static void Coeinitial_A2()
        {
            if (coeA2 != null && coeA2.Count > 0)
            {
                return;
            }

            coeA2.Add(new Coe("A2", 2, 1.881));
            coeA2.Add(new Coe("A2", 3, 1.023));
            coeA2.Add(new Coe("A2", 4, 0.729));
            coeA2.Add(new Coe("A2", 5, 0.577));
            coeA2.Add(new Coe("A2", 6, 0.483));
            coeA2.Add(new Coe("A2", 7, 0.419));
            coeA2.Add(new Coe("A2", 8, 0.373));
            coeA2.Add(new Coe("A2", 9, 0.337));
            coeA2.Add(new Coe("A2", 10, 0.308));
            coeA2.Add(new Coe("A2", 11, 0.285));
            coeA2.Add(new Coe("A2", 12, 0.266));
            coeA2.Add(new Coe("A2", 13, 0.249));
            coeA2.Add(new Coe("A2", 14, 0.235));
            coeA2.Add(new Coe("A2", 15, 0.223));
            coeA2.Add(new Coe("A2", 16, 0.212));
            coeA2.Add(new Coe("A2", 17, 0.203));
            coeA2.Add(new Coe("A2", 18, 0.194));
            coeA2.Add(new Coe("A2", 19, 0.187));
            coeA2.Add(new Coe("A2", 20, 0.18));
            coeA2.Add(new Coe("A2", 21, 0.173));
            coeA2.Add(new Coe("A2", 22, 0.167));
            coeA2.Add(new Coe("A2", 23, 0.162));
            coeA2.Add(new Coe("A2", 24, 0.157));
            coeA2.Add(new Coe("A2", 25, 0.153));
            //----------------------------------
            coeA2.Add(new Coe("A2", 26, 0.148422908));
            coeA2.Add(new Coe("A2", 27, 0.144445902));
            coeA2.Add(new Coe("A2", 28, 0.140786369));
            coeA2.Add(new Coe("A2", 29, 0.137314768));
            coeA2.Add(new Coe("A2", 30, 0.134048595));
            coeA2.Add(new Coe("A2", 31, 0.131003138));
            coeA2.Add(new Coe("A2", 32, 0.128130004));
            coeA2.Add(new Coe("A2", 33, 0.125386067));
            coeA2.Add(new Coe("A2", 34, 0.122820663));
            coeA2.Add(new Coe("A2", 35, 0.120363768));
            coeA2.Add(new Coe("A2", 36, 0.118035883));
            coeA2.Add(new Coe("A2", 37, 0.115801118));
            coeA2.Add(new Coe("A2", 38, 0.113706604));
            coeA2.Add(new Coe("A2", 39, 0.111691342));
            coeA2.Add(new Coe("A2", 40, 0.109750497));
            coeA2.Add(new Coe("A2", 41, 0.107929345));
            coeA2.Add(new Coe("A2", 42, 0.106147684));
            coeA2.Add(new Coe("A2", 43, 0.104474928));
            coeA2.Add(new Coe("A2", 44, 0.102834701));
            coeA2.Add(new Coe("A2", 45, 0.101294133));
            coeA2.Add(new Coe("A2", 46, 0.099780255));
            coeA2.Add(new Coe("A2", 47, 0.098335949));
            coeA2.Add(new Coe("A2", 48, 0.096957614));
            coeA2.Add(new Coe("A2", 49, 0.095620578));
            coeA2.Add(new Coe("A2", 50, 0.094322825));

        }

        public static void Coeinitial_A3()
        {
            if (coeA3 != null && coeA3.Count > 0)
            {
                return;
            }

            coeA3.Add(new Coe("A3", 2, 2.659));
            coeA3.Add(new Coe("A3", 3, 1.954));
            coeA3.Add(new Coe("A3", 4, 1.628));
            coeA3.Add(new Coe("A3", 5, 1.427));
            coeA3.Add(new Coe("A3", 6, 1.287));
            coeA3.Add(new Coe("A3", 7, 1.182));
            coeA3.Add(new Coe("A3", 8, 1.099));
            coeA3.Add(new Coe("A3", 9, 1.032));
            coeA3.Add(new Coe("A3", 10, 0.975));
            coeA3.Add(new Coe("A3", 11, 0.927));
            coeA3.Add(new Coe("A3", 12, 0.886));
            coeA3.Add(new Coe("A3", 13, 0.85));
            coeA3.Add(new Coe("A3", 14, 0.817));
            coeA3.Add(new Coe("A3", 15, 0.789));
            coeA3.Add(new Coe("A3", 16, 0.763));
            coeA3.Add(new Coe("A3", 17, 0.739));
            coeA3.Add(new Coe("A3", 18, 0.718));
            coeA3.Add(new Coe("A3", 19, 0.698));
            coeA3.Add(new Coe("A3", 20, 0.68));
            coeA3.Add(new Coe("A3", 21, 0.663));
            coeA3.Add(new Coe("A3", 22, 0.647));
            coeA3.Add(new Coe("A3", 23, 0.633));
            coeA3.Add(new Coe("A3", 24, 0.619));
            coeA3.Add(new Coe("A3", 25, 0.606));

        }

        public static void Coeinitial_c5()
        {
            if (coeA3 != null && coec5.Count > 0)
            {
                return;
            }

            coec5.Add(new Coe("c5", 02, 0.603));
            coec5.Add(new Coe("c5", 03, 0.463));
            coec5.Add(new Coe("c5", 04, 0.389));
            coec5.Add(new Coe("c5", 05, 0.341));
            coec5.Add(new Coe("c5", 06, 0.308));
            coec5.Add(new Coe("c5", 07, 0.282));
            coec5.Add(new Coe("c5", 08, 0.262));
            coec5.Add(new Coe("c5", 09, 0.246));
            coec5.Add(new Coe("c5", 10, 0.232));
            coec5.Add(new Coe("c5", 11, 0.22));
            coec5.Add(new Coe("c5", 12, 0.21));
            coec5.Add(new Coe("c5", 13, 0.202));
            coec5.Add(new Coe("c5", 14, 0.194));
            coec5.Add(new Coe("c5", 15, 0.187));
            coec5.Add(new Coe("c5", 16, 0.181));
            coec5.Add(new Coe("c5", 17, 0.175));
            coec5.Add(new Coe("c5", 18, 0.17));
            coec5.Add(new Coe("c5", 19, 0.166));
            coec5.Add(new Coe("c5", 20, 0.161));
            coec5.Add(new Coe("c5", 21, 0.157));
            coec5.Add(new Coe("c5", 22, 0.153));
            coec5.Add(new Coe("c5", 23, 0.15));
            coec5.Add(new Coe("c5", 24, 0.147));
            coec5.Add(new Coe("c5", 25, 0.144));


        }
    }

    /// <summary>
    /// Factors
    /// </summary>
    public struct Coe
    {
        readonly string pmode;
        readonly long pseqno;
        readonly double pnvalue;

        public Coe(string mode, long seqno, double nvalue)
        {
            this.pmode = mode;
            this.pseqno = seqno;
            this.pnvalue = nvalue;
        }

        public string Mode => this.pmode;

        public long SeqNo => this.pseqno;

        public double NValue => this.pnvalue;
    }

    /// <summary>
    /// SPC 통계 함수 정의.
    /// </summary>
    public static class SPCSta
    {
        /// <summary>
        /// 표준편차
        /// </summary>
        /// <param name="Array"></param>
        /// <param name="Ave"></param>
        /// <returns></returns>
        private static double GetStDev(double[] Array, double Ave)
        {
            return Math.Sqrt(Array.Select(val => (val - Ave) * (val - Ave)).Sum() / (Array.Length - 1));
        }

        /// <summary>
        /// 표준편차
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double? StdDev(this IEnumerable<double> values, out int errorNo, out string errorMessage)
        {
            double? ret = null;
            int count = values.Count();
            errorNo = 0;
            errorMessage = "";
            try
            {
                if (count > 1)
                {
                    //Compute the Average
                    double avg = values.Average();

                    //Perform the Sum of (value-avg)^2
                    double sum = values.Sum(d => (d - avg) * (d - avg));

                    //Put it all together
                    ret = Math.Sqrt(sum / count);
                }
                else
                {
                    errorNo = 1;
                    //측정값이 1개 임으로 표준편차를 계산 할 수 없습니다.
                    errorMessage = SpcLibMessage.common.comCpk1028;
                }

            }
            catch (Exception ex)
            {
                errorNo = 2;
                errorMessage = ex.Message;
            }

            return ret;
        }


        /// <summary>
        /// 표준표차 N-1
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double? StdDevP(this IEnumerable<double> values)
        {
            double? ret = null;
            int count = values.Count();

            if (count > 1)
            {
                //Compute the Average
                double avg = values.Average();

                //Perform the Sum of (value-avg)^2
                double sum = values.Sum(d => (d - avg) * (d - avg));

                //Put it all together
                ret = Math.Sqrt(sum / (count - 1));
            }

            return ret;
        }
        /// <summary>
        /// 표준편차 및 분산 반환.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="stdSum"></param>
        /// <returns></returns>
        public static double? StdDevPV(this IEnumerable<double> values, out double? stdSum)
        {
            double? ret = null;
            stdSum = null;
            int count = values.Count();

            if (count > 1)
            {
                //Compute the Average
                double avg = values.Average();

                //Perform the Sum of (value-avg)^2
                double sum = values.Sum(d => (d - avg) * (d - avg));
                stdSum = sum / (count - 1);
                //Put it all together
                ret = Math.Sqrt(sum / (count - 1));
            }

            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private static void button1_Click(object sender, EventArgs e)
        //{
        //    // Our sample values
        //    int[] nums = { 12, 39, 45, 47, 56 };
        //    // Get the variance of our values
        //    double varianceValue = Variance(nums);
        //    // Now calculate the standard deviation 
        //    double stndDeviation = standardDeviation(varianceValue);
        //    // Print out our result
        //    //MessageBox.Show(stndDeviation.ToString());
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private static double Variance(int[] nums)
        {
            if (nums.Length > 1)
            {
                // Get the average of the values
                double avg = GetAverage(nums);

                // Now figure out how far each point is from the mean
                // So we subtract from the number the average
                // Then raise it to the power of 2

                double sumOfSquares = 0.0;

                foreach (int num in nums)
                {
                    sumOfSquares += Math.Pow((num - avg), 2.0);
                }

                // Finally divide it by n - 1 (for standard deviation variance)
                // Or use length without subtracting one ( for population standard deviation variance)
                return sumOfSquares / (double)(nums.Length - 1);
            }

            return 0.0;
        }

        /// <summary>
        /// Square root the variance to get the standard deviation
        /// </summary>
        /// <param name="variance"></param>
        /// <returns></returns>
        private static double StandardDeviation(double variance) => Math.Sqrt(variance);

        /// <summary>
        /// Get the average of our values in the array
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private static double GetAverage(int[] nums)
        {
            int sum = 0;

            if (nums.Length > 1)
            {
                // Sum up the values

                foreach (int num in nums)
                {
                    sum += num;
                }

                // Divide by the number of values

                return sum / (double)nums.Length;

            }

            return nums[0];
        }

        /// <summary>
        /// c4 
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double GetCoe(string mode, long seqno)
        {
            Coe CoeValue = new Coe();
            double rtnVal;
            //rr = CoeTable.coe.Where(w => w.mode == "c4" && w.seqno == 3).Select(s => s.nvalue).FirstOrDefault();
            CoeTable.Coeinitial();
            //CoeTable.CoeinitialMini();
            CoeValue = CoeTable.coe.Where(w => w.Mode == mode && w.SeqNo == seqno).SingleOrDefault();

            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                rtnVal = 0.99975;//1001
            }

            return rtnVal;
        }
        
        /// <summary>
        /// d2
        /// </summary>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double GetCoed2(long seqno)
        {
            Coe CoeValue = new Coe();
            string mode = "d2";
            double rtnVal;
            CoeTable.Coeinitial_d2();
            CoeValue = CoeTable.coed2.Where(w => w.Mode == mode && w.SeqNo == seqno).SingleOrDefault();

            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                //rtnVal = 3.931;//25 기본
                //rtnVal = 4.498;//50 미니텝 기준
                rtnVal = 3.4873 + 0.0250141 * seqno - 0.00009823 * (seqno * seqno);
            }

            return rtnVal;
        }

        /// <summary>d3
        /// 
        /// </summary>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double GetCoed3(long seqno)
        {
            Coe CoeValue = new Coe();
            string mode = "d3";
            double rtnVal;
            CoeTable.Coeinitial_d3();
            CoeValue = CoeTable.coed3.Where(w => w.Mode == mode && w.SeqNo == seqno).SingleOrDefault();

            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                //rtnVal = 0.7084;//25
                rtnVal = 0.80818 - 0.0051871 * seqno + 0.00005098 * (seqno * seqno) - 0.00000019 * (seqno * seqno * seqno);
            }

            return rtnVal;
        }

        /// <summary>
        /// D3
        /// </summary>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double GetCoeD3(long seqno)
        {
            Coe CoeValue = new Coe();
            string mode = "D3";
            double rtnVal;
            CoeTable.Coeinitial_D3();
            CoeValue = CoeTable.coeD3.Where(w => w.Mode == mode && w.SeqNo == seqno).SingleOrDefault();

            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                rtnVal = 0.7084;//24
                rtnVal = 0.65252499999999991;
            }

            return rtnVal;
        }

        /// <summary>d4
        /// 
        /// </summary>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double GetCoed4(long seqno)
        {
            Coe CoeValue = new Coe();
            string mode = "d4";
            double rtnVal;
            CoeTable.Coeinitial_d4();
            CoeValue = CoeTable.coed4.Where(w => w.Mode == mode && w.SeqNo == seqno).SingleOrDefault();

            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                //rtnVal = 3.883;//25
                rtnVal = 2.88606 + 0.051313 * seqno - 0.00049243 * (seqno * seqno) + 0.00000188 * (seqno * seqno * seqno);
            }

            return rtnVal;
        }

        /// <summary>
        /// d4
        /// </summary>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double getCoeD4(long seqno)
        {
            Coe CoeValue = new Coe();
            string mode = "D4";
            double rtnVal;
            CoeTable.Coeinitial_D4();
            CoeValue = CoeTable.coeD4.Where(w => w.Mode == mode && w.SeqNo == seqno).SingleOrDefault();

            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                //rtnVal = 1.541;//24
                rtnVal = 6.917785;
            }

            return rtnVal;
        }

        /// <summary>
        /// A2
        /// </summary>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double GetCoeA2(long seqno)
        {
            Coe CoeValue = new Coe();
            string mode = "A2";
            double rtnVal;
            CoeTable.Coeinitial_A2();
            CoeValue = CoeTable.coeA2.Where(w => w.Mode == mode && w.SeqNo == seqno).SingleOrDefault();

            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                //rtnVal = 3.883;//24
                rtnVal = 0.094322825; //50
            }

            return rtnVal;
        }

        /// <summary>
        /// A3
        /// </summary>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double GetCoeA3(long seqno)
        {
            Coe CoeValue = new Coe();
            string mode = "A3";
            double rtnVal;
            CoeTable.Coeinitial_A3();
            CoeValue = CoeTable.coeA3.Where(w => w.Mode == mode && w.SeqNo == seqno).SingleOrDefault();

            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                rtnVal = 0.606;//25
            }

            return rtnVal;
        }
        /// <summary>
        /// c5
        /// </summary>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double GetCoec5(long seqno, double c4)
        {
            double rtnVal;
            rtnVal = 1 - (c4 * c4);
            rtnVal = Math.Sqrt(rtnVal);
            return rtnVal;
        }

        /// <summary>
        /// c5
        /// </summary>
        /// <param name="seqno"></param>
        /// <returns></returns>
        public static double GetCoec5(long seqno)
        {
            Coe CoeValue = new Coe();
            string mode = "c5";
            double rtnVal;
            CoeTable.Coeinitial_c5();
            CoeValue = CoeTable.coec5.Where(w => w.Mode == mode && w.SeqNo == seqno).SingleOrDefault();

            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                rtnVal = 0.144;//25
            }

            return rtnVal;
        }
    }

    /// <summary>
    /// Spc Frame 변경 자료
    /// </summary>
    public class SpcFrameChangeData
    {
        /// <summary>
        /// 재분석 구분 : true - 재분석 실행. false - 분석함.
        /// </summary>
        public bool isAgainAnalysis;
        public bool isAgainAnalysisXBar;
        public bool isAgainAnalysisCpk;
        public bool isAgainAnalysisButtonXBar;
        public bool isAgainAnalysisButtonCpk;
        public bool isAgainAnalysisOverRules; 
        /// <summary>
        /// SPC Option
        /// </summary>
        public SPCOption SPCOption;
    }

    /// <summary>
    /// Navigator Row Enter 이벤트 Parameter
    /// </summary>
    public class SpcEventNavigatorRowEnter
    {
        public long seqStart;
        public long seqEnd;
    }

}
