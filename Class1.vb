' NX 12.0.0.27
' Journal created by wangjian on Thu Sep 21 21:25:03 2023 Malay Peninsula Standard Time
' This code to generate point set on face 
' Tested on 2020 Nov 03
' NX12 + VS 2019
' Default file path is the NX part file path

Imports System
Imports NXOpen
Imports NXOpen.Features
Imports NXOpen.UF
Imports System.String
Imports NXOpenUI
Imports System.IO
Class Gen_ptset_export_txt
    Public Shared theSession As NXOpen.Session = NXOpen.Session.GetSession()
    Public Shared theUfSession As UFSession = UFSession.GetUFSession()
    Public Shared workPart As NXOpen.Part = theSession.Parts.Work
    Public Shared displayPart As NXOpen.Part = theSession.Parts.Display
    Public Shared lw As ListingWindow = theSession.ListingWindow
    Public Shared u1, v1 As String
    Public Shared Sub Main(ByVal args() As String)
        lw.Open()
        theSession.ApplicationSwitchImmediate("UG_APP_MODELING")
#Region "---------input U, V value---------"
        ' in NX CADCAM, point set created by U,V grid
        ' U: the number of points will be created along U direction
        ' V: the number of points will be created along V direction

        Dim theUISession As UI = UI.GetUI
        Dim answer2 As String
        MsgBox("input number of point along U, V direction")
        answer2 = NXInputBox.GetInputString("U, V")
        Dim strarr As String() = answer2.Split(","c)
        u1 = strarr(0)
        v1 = strarr(1)
        'u1 = "6"
        'v1 = "5"
#End Region

#Region "----------face selection (select one face), for point set create on -----------"
        Dim theFace As TaggedObject = SelectFace("select one face")
        If theFace Is Nothing Then
            Return
        End If

        'lw.WriteLine("theface tag: " & theFace.Tag.ToString)
        'lw.WriteLine("theface gettype: " & theFace.GetType.ToString)
        'lw.WriteLine("theface name: " & theFace.GetType.ToString)

        Dim face01 As Face = theSession.GetObjectManager.GetTaggedObject(theFace.Tag)
        'lw.WriteLine("face01 tag: " & face01.Tag.ToString)
        'lw.WriteLine("face01 get type: " & face01.GetType.ToString)
        'lw.WriteLine("face01 journalidentifier: " & face01.JournalIdentifier)
#End Region

#Region "------------Create pointset along U,V direction------------"
        Dim temp_pt_set As PointSet = create_pts_on_face(face01)
        'lw.WriteLine("temp_pt_set tag: " & temp_pt_set.Tag.ToString)
        'lw.WriteLine("temp_pt_set type: " & temp_pt_set.GetType.ToString)
        'lw.WriteLine("temp_pt_set name: " & temp_pt_set.Name.ToString)
        'lw.WriteLine("temp_pt_set journalidentifier: " & temp_pt_set.JournalIdentifier.ToString)
#End Region

        '---------trim point cloud by face boundary and export points to txt file-----------------
        ' for each point in point set, skip certain points if it's distance to face is lesser than threshold,  we take 0.001mm
        ' write points to txt file
        pt_filter(temp_pt_set, theFace)

        '-------delete pointset---------
        ' delete the original point set created along U,V
        dele_obj(temp_pt_set)

        '------- Read points from txt file exported -----------
        read_pts_from_txtfile(curr_path)
    End Sub
    '----------create point set on face along U,V direction, recorded with nxjournal and simplyfied-----------
    Public Shared Function create_pts_on_face(face1 As Face) As PointSet
        Dim nullNXOpen_Features_PointSet As NXOpen.Features.PointSet = Nothing
        Dim pointSetBuilder1 As NXOpen.Features.PointSetBuilder = Nothing
        pointSetBuilder1 = workPart.Features.CreatePointSetBuilder(nullNXOpen_Features_PointSet)
        pointSetBuilder1.Type = NXOpen.Features.PointSetBuilder.Types.FacePoints
        pointSetBuilder1.NumberOfPointsExpression.RightHandSide = "2"
        pointSetBuilder1.StartPercentage.RightHandSide = "0"
        pointSetBuilder1.EndPercentage.RightHandSide = "100"
        pointSetBuilder1.Ratio.RightHandSide = "1"
        pointSetBuilder1.ChordalTolerance.RightHandSide = "2.54"
        pointSetBuilder1.ArcLength.RightHandSide = "1"
        pointSetBuilder1.NumberOfPointsInUDirectionExpression.RightHandSide = u1
        pointSetBuilder1.NumberOfPointsInVDirectionExpression.RightHandSide = v1
        pointSetBuilder1.PatternLimitsBy = NXOpen.Features.PointSetBuilder.PatternLimitsType.Percentages
        pointSetBuilder1.PatternLimitsStartingUValue.RightHandSide = "0"
        pointSetBuilder1.PatternLimitsEndingUValue.RightHandSide = "100"
        pointSetBuilder1.PatternLimitsStartingVValue.RightHandSide = "0"
        pointSetBuilder1.PatternLimitsEndingVValue.RightHandSide = "100"
        Dim nullNXOpen_Unit As NXOpen.Unit = Nothing
        Dim expression1 As NXOpen.Expression = Nothing
        expression1 = workPart.Expressions.CreateSystemExpressionWithUnits("50", nullNXOpen_Unit)
        pointSetBuilder1.CurvePercentageList.Append(expression1)

        Dim pointSetFacePercentageBuilder1 As NXOpen.Features.PointSetFacePercentageBuilder = Nothing
        pointSetFacePercentageBuilder1 = pointSetBuilder1.CreateFacePercentageListItem()
        pointSetBuilder1.FacePercentageList.Append(pointSetFacePercentageBuilder1)
        pointSetBuilder1.SingleFaceObject.Value = face1

        Dim nXObject1 As NXOpen.NXObject = Nothing
        nXObject1 = pointSetBuilder1.Commit()
        pointSetBuilder1.Destroy()

        Return nXObject1
    End Function
    '-----------face selection ---------
    Public Shared Function SelectFace(ByVal propt As String) As TaggedObject
        Dim theUI As UI = UI.GetUI
        Dim title As String = "Select one face"
        Dim includeFeatures As Boolean = False
        Dim keepHighlighted As Boolean = False
        Dim selAction As Selection.SelectionAction = Selection.SelectionAction.ClearAndEnableSpecific
        Dim scope As Selection.SelectionScope = Selection.SelectionScope.AnyInAssembly
        Dim selectionMask(0) As Selection.MaskTriple
        Dim selectedObject As TaggedObject = Nothing
        Dim selectedFaces As New List(Of Face)
        Dim cur As Point3d

        With selectionMask(0)
            .Type = UFConstants.UF_face_type
            .Subtype = UFConstants.UF_UI_SEL_FEATURE_BODY
            ' .SolidBodySubtype = UFConstants.UF_b_surface_subtype
        End With

        Dim responce1 As Selection.Response = theUI.SelectionManager.SelectTaggedObject(
            propt, title, scope, selAction, includeFeatures, keepHighlighted, selectionMask, selectedObject, cur)

        If responce1 = Selection.Response.Ok Or Selection.Response.ObjectSelected Then
            'For Each item As TaggedObject In selectedObjects
            Return selectedObject
            'Next
        Else
            Return Nothing
        End If
    End Function

    '------------- check path of NX part --------- 
    Public Shared Function curr_path() As String
        Dim CurrentPath As String = displayPart.FullPath
        Dim ind01 As Integer = CurrentPath.IndexOf("\")
        Dim str_temp As String = CurrentPath
        Dim str_int As Integer = str_temp.IndexOf("\"c)  '
        Dim new_str As String
        Do While (str_int <> -1)
            new_str = str_temp.Substring(str_int)
            str_int = str_temp.IndexOf("\"c, str_int + 1)
        Loop
        Dim temp_path As String = CurrentPath.Substring(0, (CurrentPath.Length - new_str.Length))
        Dim x As String = temp_path + "\" + "pts_exported.txt"
        Return x
    End Function
    '---------------check dist from pt to face, skip certain pts and write rest to text file----------
    Public Shared Sub pt_filter(pt_cloud As NXOpen.Features.PointSet, target As Face)
        'the sub will remove point which located on trimmed area by checking dist from pt to face 
        Dim guess1(2) As Double
        Dim guess2(2) As Double
        Dim pt1(2) As Double
        Dim pt2(2) As Double
        Dim minDist_pt_to_body As Double
        lw.WriteFullline("filepath: " & curr_path())
        Using writer As New StreamWriter(curr_path())
            For Each pt As Point In pt_cloud.GetEntities
                theUfSession.Modl.AskMinimumDist(pt.Tag, target.Tag, 0, guess1, 0, guess2, minDist_pt_to_body, pt1, pt2)
                If minDist_pt_to_body < 0.001 Then
                    Dim obj As NXOpen.TaggedObject = NXOpen.Utilities.NXObjectManager.Get(pt.Tag)
                    Dim myPoint As NXOpen.Point = CType(obj, NXOpen.Point)
                    writer.WriteLine(myPoint.Coordinates.X & "," & myPoint.Coordinates.Y & "," & myPoint.Coordinates.Z)
                Else
                    'lw.WriteLine("dist larger than 0.001")
                End If
            Next
        End Using
        lw.WriteLine("pts have exported to file")
    End Sub
    '----------- delete tagged objects------------
    Public Shared Sub dele_obj(obj_tobe_deleted As TaggedObject) ' recorded from NX journal
        Dim markId1 As Session.UndoMarkId
        markId1 = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Delete point cloud")

        Dim notifyOnDelete1 As Boolean
        notifyOnDelete1 = theSession.Preferences.Modeling.NotifyOnDelete

        theSession.UpdateManager.ClearErrorList()

        Dim nErrs1 As Integer
        nErrs1 = theSession.UpdateManager.AddToDeleteList(obj_tobe_deleted)

        Dim notifyOnDelete2 As Boolean
        notifyOnDelete2 = theSession.Preferences.Modeling.NotifyOnDelete

        Dim nErrs2 As Integer
        nErrs2 = theSession.UpdateManager.DoUpdate(markId1)
    End Sub
    '-----------import points from text file and display in NX CADCAM----------
    Public Shared Sub read_pts_from_txtfile(filepath As String)  ' recorded from NX journal

        Dim pointsFromFileBuilder1 As NXOpen.GeometricUtilities.PointsFromFileBuilder = Nothing
        pointsFromFileBuilder1 = workPart.CreatePointsFromFileBuilder()
        pointsFromFileBuilder1.FileName = filepath
        pointsFromFileBuilder1.CoordinateOption = NXOpen.GeometricUtilities.PointsFromFileBuilder.Options.Absolute

        Dim nXObject1 As NXOpen.NXObject = Nothing
        nXObject1 = pointsFromFileBuilder1.Commit()
        pointsFromFileBuilder1.Destroy()
    End Sub
    Public Shared Function GetUnloadOption(ByVal dummy As String) As Integer
        'Unloads the image immediately after execution within NX
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.Immediately
    End Function
End Class