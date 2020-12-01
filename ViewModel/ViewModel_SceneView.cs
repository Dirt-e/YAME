using MOTUS.Model;
using MOTUS.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MOTUS.ViewModel
{
    //public class ViewModel_Sceneview : _ViewModel
    //{
    //    #region BaseConnectors (6)
    //    Transform3D _base1;
    //    public Transform3D Base1
    //    {
    //        get { return _base1; }
    //        set { _base1 = value; OnPropertyChanged("Base1"); }
    //    }
    //    Transform3D _base2;
    //    public Transform3D Base2
    //    {
    //        get { return _base2; }
    //        set { _base2 = value; OnPropertyChanged("Base2"); }
    //    }
    //    Transform3D _base3;
    //    public Transform3D Base3
    //    {
    //        get { return _base3; }
    //        set { _base3 = value; OnPropertyChanged("Base3"); }
    //    }
    //    Transform3D _base4;
    //    public Transform3D Base4
    //    {
    //        get { return _base4; }
    //        set { _base4 = value; OnPropertyChanged("Base4"); }
    //    }
    //    Transform3D _base5;
    //    public Transform3D Base5
    //    {
    //        get { return _base5; }
    //        set { _base5 = value; OnPropertyChanged("Base5"); }
    //    }
    //    Transform3D _base6;
    //    public Transform3D Base6
    //    {
    //        get { return _base6; }
    //        set { _base6 = value; OnPropertyChanged("Base6"); }
    //    }
    //    #endregion

    //    #region Platforms (6)
    //    Transform3D _plat_fix_pause;
    //    public Transform3D PlatFixPause
    //    {
    //        get { return _plat_fix_pause; }
    //        set { _plat_fix_pause = value; OnPropertyChanged("PlatFixPause"); } //50% extension
    //    }
    //    Transform3D _plat_cor;
    //    public Transform3D PlatCoR
    //    {
    //        get { return _plat_cor; }
    //        set { _plat_cor = value; OnPropertyChanged("PlatCoR"); }
    //    }
    //    Transform3D _plat_lfc;
    //    public Transform3D PlatLFC
    //    {
    //        get { return _plat_lfc; }
    //        set { _plat_lfc = value; OnPropertyChanged("PlatLFC"); }
    //    }
    //    Transform3D _plat_hfc;
    //    public Transform3D PlatHFC
    //    {
    //        get { return _plat_hfc; }
    //        set { _plat_hfc = value; OnPropertyChanged("PlatHFC"); }
    //    }
    //    Transform3D _plat_motion;
    //    public Transform3D PlatMotion
    //    {
    //        get { return _plat_motion; }
    //        set { _plat_motion = value; OnPropertyChanged("PlatMotion"); }
    //    }
    //    //---------------------------------
    //    Transform3D _plat_fix_park;
    //    public Transform3D PlatFixPark
    //    {
    //        get { return _plat_fix_park; }
    //        set { _plat_fix_park = value; OnPropertyChanged("PlatFixPark"); }   //Minimum extension
    //    }
    //    #endregion

    //    #region Floating Platform (1)
    //    Transform3D _plat_float_physical;
    //    public Transform3D PlatFloatPhysical
    //    {
    //        get { return _plat_float_physical; }
    //        set { _plat_float_physical = value; OnPropertyChanged("PlatFloatPhysical"); }
    //    }
    //    #endregion

    //    #region UpperConnectors (6)
    //    Transform3D _upper1;
    //    public Transform3D Upper1
    //    {
    //        get { return _upper1; }
    //        set { _upper1 = value; OnPropertyChanged("Upper1"); }
    //    }
    //    Transform3D _upper2;
    //    public Transform3D Upper2
    //    {
    //        get { return _upper2; }
    //        set { _upper2 = value; OnPropertyChanged("Upper2"); }
    //    }
    //    Transform3D _upper3;
    //    public Transform3D Upper3
    //    {
    //        get { return _upper3; }
    //        set { _upper3 = value; OnPropertyChanged("Upper3"); }
    //    }
    //    Transform3D _upper4;
    //    public Transform3D Upper4
    //    {
    //        get { return _upper4; }
    //        set { _upper4 = value; OnPropertyChanged("Upper4"); }
    //    }
    //    Transform3D _upper5;
    //    public Transform3D Upper5
    //    {
    //        get { return _upper5; }
    //        set { _upper5 = value; OnPropertyChanged("Upper5"); }
    //    }
    //    Transform3D _upper6;
    //    public Transform3D Upper6
    //    {
    //        get { return _upper6; }
    //        set { _upper6 = value; OnPropertyChanged("Upper6"); }
    //    }
    //    #endregion

    //    #region WORLD POINT3Ds FOR SPHERES (BASE)
    //    Point3D _worldcoord_Base1;
    //    public Point3D WorldCoord_Base1
    //    {
    //        get { return _worldcoord_Base1; }
    //        set { _worldcoord_Base1 = value; OnPropertyChanged("WorldCoord_Base1"); }
    //    }
    //    Point3D _worldcoord_Base2;
    //    public Point3D WorldCoord_Base2
    //    {
    //        get { return _worldcoord_Base2; }
    //        set { _worldcoord_Base2 = value; OnPropertyChanged("WorldCoord_Base2"); }
    //    }
    //    Point3D _worldcoord_Base3;
    //    public Point3D WorldCoord_Base3
    //    {
    //        get { return _worldcoord_Base3; }
    //        set { _worldcoord_Base3 = value; OnPropertyChanged("WorldCoord_Base3"); }
    //    }
    //    Point3D _worldcoord_Base4;
    //    public Point3D WorldCoord_Base4
    //    {
    //        get { return _worldcoord_Base4; }
    //        set { _worldcoord_Base4 = value; OnPropertyChanged("WorldCoord_Base4"); }
    //    }
    //    Point3D _worldcoord_Base5;
    //    public Point3D WorldCoord_Base5
    //    {
    //        get { return _worldcoord_Base5; }
    //        set { _worldcoord_Base5 = value; OnPropertyChanged("WorldCoord_Base5"); }
    //    }
    //    Point3D _worldcoord_Base6;
    //    public Point3D WorldCoord_Base6
    //    {
    //        get { return _worldcoord_Base6; }
    //        set { _worldcoord_Base6 = value; OnPropertyChanged("WorldCoord_Base6"); }
    //    }
    //    #endregion

    //    #region WORLD POINT3Ds FOR SPHERES (UPPER)
    //    Point3D _worldcoord_Upper1;
    //    public Point3D WorldCoord_Upper1
    //    {
    //        get { return _worldcoord_Upper1; }
    //        set { _worldcoord_Upper1 = value; OnPropertyChanged("WorldCoord_Upper1"); }
    //    }
    //    Point3D _worldcoord_Upper2;
    //    public Point3D WorldCoord_Upper2
    //    {
    //        get { return _worldcoord_Upper2; }
    //        set { _worldcoord_Upper2 = value; OnPropertyChanged("WorldCoord_Upper2"); }
    //    }
    //    Point3D _worldcoord_Upper3;
    //    public Point3D WorldCoord_Upper3
    //    {
    //        get { return _worldcoord_Upper3; }
    //        set { _worldcoord_Upper3 = value; OnPropertyChanged("WorldCoord_Upper3"); }
    //    }
    //    Point3D _worldcoord_Upper4;
    //    public Point3D WorldCoord_Upper4
    //    {
    //        get { return _worldcoord_Upper4; }
    //        set { _worldcoord_Upper4 = value; OnPropertyChanged("WorldCoord_Upper4"); }
    //    }
    //    Point3D _worldcoord_Upper5;
    //    public Point3D WorldCoord_Upper5
    //    {
    //        get { return _worldcoord_Upper5; }
    //        set { _worldcoord_Upper5 = value; OnPropertyChanged("WorldCoord_Upper5"); }
    //    }
    //    Point3D _worldcoord_Upper6;
    //    public Point3D WorldCoord_Upper6
    //    {
    //        get { return _worldcoord_Upper6; }
    //        set { _worldcoord_Upper6 = value; OnPropertyChanged("WorldCoord_Upper6"); }
    //    }
    //    #endregion

    //    #region Actuators (6)
    //    Actuator A1 = new Actuator();
    //    #endregion



    //    public ViewModel_Sceneview(Engine e)
    //    {
    //        base.engine = e;
    //    }
    //}
}
