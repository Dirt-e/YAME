using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using YAME.Model;

public class Utility
{
    public static float DEG_from_RAD(float R)
    {
        return Convert.ToSingle(R / (2 * Math.PI) * 360);
    }
    public static float RAD_from_DEG(float D)
    {
        return Convert.ToSingle((D / 360.0f) * 2 * Math.PI);
    }
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapter found!");
    }
    public static float InvertHeading(float hdg)
    {
        if (hdg < 180) return hdg + 180;
        else return hdg - 180;
    }

    public static int Clamp(int value, int min, int max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
    public static float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
    public static double Clamp(double value, double min, double max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    public static Quaternion QuaternionFrom(double yaw, double pitch, double roll)
    {
        // All angles in radians!
        //Confirmed for HelixToolkit

        double CosYaw = Math.Cos(yaw / 2);
        double SinYaw = Math.Sin(yaw / 2);
        double CosPitch = Math.Cos(pitch / 2);
        double SinPitch = Math.Sin(pitch / 2);
        double CosRoll = Math.Cos(roll / 2);
        double SinRoll = Math.Sin(roll / 2);
        double CosYawCosPitch = CosYaw * CosPitch;
        double SinYawSinPitch = SinYaw * SinPitch;

        double w = CosYawCosPitch * CosRoll - SinYawSinPitch * SinRoll;
        double y = CosYawCosPitch * SinRoll + SinYawSinPitch * CosRoll;
        double z = SinYaw * CosPitch * CosRoll + CosYaw * SinPitch * SinRoll;
        double x = CosYaw * SinPitch * CosRoll - SinYaw * CosPitch * SinRoll;

        return new Quaternion(x, y, z, w);
    }
    public static Quaternion QuaternionFrom(Vector3D eulervector)
    {
        return QuaternionFrom(eulervector.X, eulervector.Y, eulervector.Z);
    }
    public static Quaternion QuaternionFrom(Matrix3D m)
    {
        Vector3D Eulervector = EulerFrom(m);

        return QuaternionFrom(Eulervector);

    }

    public static double YawFrom(Matrix3D m)
    {
        //Tested in WPF Helix :-)
        return Math.Atan2(m.M21, m.M22);
    }
    public static double PitchFrom(Matrix3D m)
    {
        //Tested in WPF Helix :-)
        return Math.Asin(m.M23);
    }
    public static double BankFrom(Matrix3D m)
    {
        ////Tested in WPF Helix :-)
        return Math.Atan2(m.M13, m.M33);
    }

    public static Vector3D EulerFrom(Matrix3D m)
    {
        Vector3D EulerVector = new Vector3D();
        EulerVector.X = YawFrom(m);
        EulerVector.Y = PitchFrom(m);
        EulerVector.Z = BankFrom(m);

        return EulerVector;
    }
    public static Vector3D EulerFrom(Quaternion q)
    {
        QuaternionRotation3D QR3D = new QuaternionRotation3D(q);
        RotateTransform3D RtTF = new RotateTransform3D(QR3D);
        Matrix3D m = RtTF.Value;

        return EulerFrom(m);
    }

    public static Point3D PointFrom(Transform3D TF)
    {
        float x = (float)TF.Value.OffsetX;
        float y = (float)TF.Value.OffsetY;
        float z = (float)TF.Value.OffsetZ;

        return new Point3D(x, y, z);
    }

    public static float DistanceBetween(Point3D p1, Point3D p2)
    {
        double delta_x = p1.X - p2.X;
        double delta_y = p1.Y - p2.Y;
        double delta_z = p1.Z - p2.Z;

        return (float)Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2) + Math.Pow(delta_z, 2));
    }

    public static byte[] GenerateBytes_16bit_from(float util)
    {
        if      (util <= 0.0f) return new byte[2] { 0, 0 };            //return a min value
        else if (util >= 1.0f) return new byte[2] { 255, 255 };        //return a max value
        else
        {
            UInt16 value;
            value = (UInt16)(UInt16.MaxValue * util);
            Byte[] Bytes = BitConverter.GetBytes(value);
            Array.Reverse(Bytes);
            return Bytes;
        }
    }
    public static byte[] GenerateBytes_24bit_from(float util)
    {
        if (util <= 0.0f) return new byte[3] { 0, 0, 0 };
        else if (util >= 1.0f) return new byte[3] { 255, 255, 255 };
        else
        {
            UInt32 value = (UInt32)(16777216 * util);
            Byte[] Bytes_4 = BitConverter.GetBytes(value);
            Byte[] Bytes_3 = new byte[3] { Bytes_4[2], Bytes_4[1], Bytes_4[0] };
            return Bytes_3;
        }
    }

    //Lerps:
    public static float Lerp(float v1, float v2, float t)
    {
        return v1 + ((v2 - v1) * t);
    }
    public static double cubicInterpolate(double p0, double p1, double p2, double p3, double x)
    {
        return p1 + 0.5 * x * (p2 - p0 + x * (2.0 * p0 - 5.0 * p1 + 4.0 * p2 - p3 + x * (3.0 * (p1 - p2) + p3 - p0)));
    }
    public static float QuadraticInterpolation(float v0, float v1, float v2, float t)
    {
        var v01 = Lerp(v0, v1, t);
        var v12 = Lerp(v1, v2, t);
        return Lerp(v01, v12, t);
    }
    public static float CosInterpolation(float t)
    {
        t = (float)-Math.Cos(t * Math.PI);  // [-1, 1]
        return (t + 1) / 2;                 // [0, 1]
    }
    public static float SmoothStep(float t)
    {
        return t * t * (3 - (2 * t));
    }
    public static float PerlinSmoothStep(float t)
    {
        // Ken Perlin's version
        return t * t * t * ((t * ((6 * t) - 15)) + 10);
    }
    public static Vector3D Lerp(Vector3D vector1, Vector3D vector2, float ratio)
    {
        float x = (float)(vector1.X + (vector2.X - vector1.X) * ratio);
        float y = (float)(vector1.Y + (vector2.Y - vector1.Y) * ratio);
        float z = (float)(vector1.Z + (vector2.Z - vector1.Z) * ratio);

        return new Vector3D(x, y, z);
    }
    public static Point3D Lerp(Point3D point1, Point3D point2, float ratio)
    {
        float x = (float)(point1.X + (point2.X - point1.X) * ratio);
        float y = (float)(point1.Y + (point2.Y - point1.Y) * ratio);
        float z = (float)(point1.Z + (point2.Z - point1.Z) * ratio);

        return new Point3D(x, y, z);
    }
    public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float ratio)
    {
        float x = (float)(quaternion1.X + (quaternion2.X - quaternion1.X) * ratio);
        float y = -(float)(quaternion1.Y + (quaternion2.Y - quaternion1.Y) * ratio);
        float z = -(float)(quaternion1.Z + (quaternion2.Z - quaternion1.Z) * ratio);
        float w = (float)(quaternion1.W + (quaternion2.W - quaternion1.W) * ratio);

        return new Quaternion(x, y, z, w);
    }
    public static Quaternion Slerp(Quaternion qa, Quaternion qb, double t)
    {
        // Quaternionernion to return
        Quaternion qm = new Quaternion();
        // Calculate angle between them.
        double cosHalfTheta = qa.W * qb.W + qa.X * qb.X + qa.Y * qb.Y + qa.Z * qb.Z;
        // if qa=qb or qa=-qb then theta = 0 and we can return qa
        if (Math.Abs(cosHalfTheta) >= 1.0)
        {
            qm.W = qa.W; qm.X = qa.X; qm.Y = qa.Y; qm.Z = qa.Z;
            return qm;
        }
        // Calculate temporary values.
        double halfTheta = Math.Acos(cosHalfTheta);
        double sinHalfTheta = Math.Sqrt(1.0 - cosHalfTheta * cosHalfTheta);
        // if theta = 180 degrees then result is not fully defined
        // we could rotate around any axis normal to qa or qb
        if (Math.Abs(sinHalfTheta) < 0.001)
        {
            qm.W = (qa.W * 0.5 + qb.W * 0.5);
            qm.X = (qa.X * 0.5 + qb.X * 0.5);
            qm.Y = (qa.Y * 0.5 + qb.Y * 0.5);
            qm.Z = (qa.Z * 0.5 + qb.Z * 0.5);
            return qm;
        }
        double ratioA = Math.Sin((1 - t) * halfTheta) / sinHalfTheta;
        double ratioB = Math.Sin(t * halfTheta) / sinHalfTheta;
        //calculate Quaternion.
        qm.W = (qa.W * ratioA + qb.W * ratioB);
        qm.X = (qa.X * ratioA + qb.X * ratioB);
        qm.Y = -(qa.Y * ratioA + qb.Y * ratioB);
        qm.Z = -(qa.Z * ratioA + qb.Z * ratioB);

        return qm;
    }
    public static Transform3D Lerp(Transform3D tf1, Transform3D tf2, float ratio)
    {
        //Translations:
        Vector3D vec1 = new Vector3D(   tf1.Value.OffsetX,
                                        tf1.Value.OffsetY,
                                        tf1.Value.OffsetZ);
        Vector3D vec2 = new Vector3D(   tf2.Value.OffsetX,
                                        tf2.Value.OffsetY,
                                        tf2.Value.OffsetZ);
        Vector3D result = Lerp(vec1, vec2, ratio);
        TranslateTransform3D Translation = new TranslateTransform3D(result);

        //Rotations:
        Quaternion Q1 = QuaternionFrom(tf1.Value);
        Quaternion Q2 = QuaternionFrom(tf2.Value);
        Quaternion Q3 = Slerp(Q1, Q2, ratio);
        QuaternionRotation3D QR = new QuaternionRotation3D(Q3);
        RotateTransform3D Rotation = new RotateTransform3D(QR);

        //Both together:
        Transform3DGroup TotalTransform = new Transform3DGroup();
        TotalTransform.Children.Add(Rotation);
        TotalTransform.Children.Add(Translation);

        return TotalTransform;
    }

    //Coloring 3d objects:
    public static void SetColor(ModelVisual3D ModVis, Color c)
    {
        if (ModVis.Content is GeometryModel3D)              //Is it a slingle geometry?
        {
            var geo = ModVis.Content as GeometryModel3D;
            geo.Material = new DiffuseMaterial(new SolidColorBrush(c));
            geo.BackMaterial = geo.Material;
        }
        else if (ModVis.Content is Model3DGroup)            //...or are there more?
        {
            var Group = ModVis.Content as Model3DGroup;
            foreach (var Child in Group.Children)            //Color them all!
            {
                var geo = Child as GeometryModel3D;
                geo.Material = new DiffuseMaterial(new SolidColorBrush(c));
                geo.BackMaterial = geo.Material;
            }
        }
    }
    public static void SetOpacity(ModelVisual3D ModVis, float opacity)
    {
        if (ModVis.Content is GeometryModel3D)              //Is it a slingle geometry?
        {
            var geo = ModVis.Content as GeometryModel3D;
            var mat = geo.Material as DiffuseMaterial;
            var scb = mat.Brush as SolidColorBrush;

            scb.Opacity = opacity;
            geo.BackMaterial = geo.Material;
        }
        else if (ModVis.Content is Model3DGroup)                    //...or are there more?
        {
            var Group = ModVis.Content as Model3DGroup;
            foreach (var Child in Group.Children)                   //Color them all!
            {
                var geo = Child as GeometryModel3D;
                var mat = geo.Material as DiffuseMaterial;
                var scb = mat.Brush as SolidColorBrush;

                scb.Opacity = opacity;
                geo.BackMaterial = geo.Material;
            }
        }
    }
    public static void SetAlpha(ModelVisual3D ModVis, byte alpha)
    {
        if (ModVis.Content is GeometryModel3D)              //Is it a slingle geometry?
        {
            var geo = ModVis.Content as GeometryModel3D;
            var mat = geo.Material as DiffuseMaterial;
            var scb = mat.Brush as SolidColorBrush;

            byte a = alpha;
            byte r = scb.Color.R;
            byte g = scb.Color.G;
            byte b = scb.Color.B;

            scb.Color = Color.FromArgb(a, r, g, b);
            geo.BackMaterial = geo.Material;
        }
        else if (ModVis.Content is Model3DGroup)                    //...or are there more?
        {
            var Group = ModVis.Content as Model3DGroup;
            foreach (var Child in Group.Children)                   //Color them all!
            {
                var geo = Child as GeometryModel3D;
                var mat = geo.Material as DiffuseMaterial;
                var scb = mat.Brush as SolidColorBrush;

                byte a = alpha;
                byte r = scb.Color.R;
                byte g = scb.Color.G;
                byte b = scb.Color.B;

                scb.Color = Color.FromArgb(a, r, g, b);
                geo.BackMaterial = geo.Material;
            }
        }
    }
}


    



