' NX 12.0.0.27
' Journal created by wjian on Sun Jul  9 23:29:19 2023 Malay Peninsula Standard Time
'
Imports System
Imports NXOpen

Module NXJournal
Sub Main (ByVal args() As String) 

Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
Dim workPart As NXOpen.Part = theSession.Parts.Work

Dim displayPart As NXOpen.Part = theSession.Parts.Display

' ----------------------------------------------
'   Menu: Insert->Design Feature->Extrude...
' ----------------------------------------------
Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")

Dim nullNXOpen_Features_Feature As NXOpen.Features.Feature = Nothing

Dim extrudeBuilder1 As NXOpen.Features.ExtrudeBuilder = Nothing
extrudeBuilder1 = workPart.Features.CreateExtrudeBuilder(nullNXOpen_Features_Feature)

Dim section1 As NXOpen.Section = Nothing
section1 = workPart.Sections.CreateSection(0.0094999999999999998, 0.01, 0.5)

extrudeBuilder1.Section = section1

extrudeBuilder1.AllowSelfIntersectingSection(True)

Dim unit1 As NXOpen.Unit = Nothing
unit1 = extrudeBuilder1.Draft.FrontDraftAngle.Units

Dim expression1 As NXOpen.Expression = Nothing
expression1 = workPart.Expressions.CreateSystemExpressionWithUnits("2.00", unit1)

extrudeBuilder1.DistanceTolerance = 0.01

extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create

Dim targetBodies1(0) As NXOpen.Body
Dim nullNXOpen_Body As NXOpen.Body = Nothing

targetBodies1(0) = nullNXOpen_Body
extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies1)

extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "0"

extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = "60"

extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create

Dim targetBodies2(0) As NXOpen.Body
targetBodies2(0) = nullNXOpen_Body
extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies2)

extrudeBuilder1.Draft.FrontDraftAngle.RightHandSide = "2"

extrudeBuilder1.Draft.BackDraftAngle.RightHandSide = "2"

extrudeBuilder1.Offset.StartOffset.RightHandSide = "0"

extrudeBuilder1.Offset.EndOffset.RightHandSide = "5"

Dim smartVolumeProfileBuilder1 As NXOpen.GeometricUtilities.SmartVolumeProfileBuilder = Nothing
smartVolumeProfileBuilder1 = extrudeBuilder1.SmartVolumeProfile

smartVolumeProfileBuilder1.OpenProfileSmartVolumeOption = False

smartVolumeProfileBuilder1.CloseProfileRule = NXOpen.GeometricUtilities.SmartVolumeProfileBuilder.CloseProfileRuleType.Fci

theSession.SetUndoMarkName(markId1, "Extrude Dialog")

section1.DistanceTolerance = 0.01

section1.ChainingTolerance = 0.0094999999999999998

section1.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves)

Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "section mark")

Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, Nothing)

Dim curves1(0) As NXOpen.IBaseCurve
Dim associativeLine1 As NXOpen.Features.AssociativeLine = CType(workPart.Features.FindObject("LINE(53)"), NXOpen.Features.AssociativeLine)

Dim line1 As NXOpen.Line = CType(associativeLine1.FindObject("CURVE 1"), NXOpen.Line)

curves1(0) = line1
Dim curveDumbRule1 As NXOpen.CurveDumbRule = Nothing
curveDumbRule1 = workPart.ScRuleFactory.CreateRuleBaseCurveDumb(curves1)

section1.AllowSelfIntersection(True)

Dim rules1(0) As NXOpen.SelectionIntentRule
rules1(0) = curveDumbRule1
Dim nullNXOpen_NXObject As NXOpen.NXObject = Nothing

Dim helpPoint1 As NXOpen.Point3d = New NXOpen.Point3d(111.90210981799564, 249.27954772547571, -1.1368683772161603e-13)
section1.AddToSection(rules1, line1, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint1, NXOpen.Section.Mode.Create, False)

theSession.DeleteUndoMark(markId3, Nothing)

Dim origin1 As NXOpen.Point3d = New NXOpen.Point3d(76.967649999999992, 239.69, 0.0)
Dim vector1 As NXOpen.Vector3d = New NXOpen.Vector3d(0.0, 0.0, 1.0)
Dim direction1 As NXOpen.Direction = Nothing
direction1 = workPart.Directions.CreateDirection(origin1, vector1, NXOpen.SmartObject.UpdateOption.WithinModeling)

extrudeBuilder1.Direction = direction1

Dim unit2 As NXOpen.Unit = Nothing
unit2 = extrudeBuilder1.Offset.StartOffset.Units

Dim expression2 As NXOpen.Expression = Nothing
expression2 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit2)

theSession.DeleteUndoMark(markId2, Nothing)

Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "section mark")

Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, Nothing)

Dim curves2(0) As NXOpen.IBaseCurve
Dim associativeLine2 As NXOpen.Features.AssociativeLine = CType(workPart.Features.FindObject("LINE(54)"), NXOpen.Features.AssociativeLine)

Dim line2 As NXOpen.Line = CType(associativeLine2.FindObject("CURVE 1"), NXOpen.Line)

curves2(0) = line2
Dim curveDumbRule2 As NXOpen.CurveDumbRule = Nothing
curveDumbRule2 = workPart.ScRuleFactory.CreateRuleBaseCurveDumb(curves2)

section1.AllowSelfIntersection(True)

Dim rules2(0) As NXOpen.SelectionIntentRule
rules2(0) = curveDumbRule2
Dim helpPoint2 As NXOpen.Point3d = New NXOpen.Point3d(121.11501033077786, 220.68729856188705, -9.9475983006414026e-14)
section1.AddToSection(rules2, line2, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint2, NXOpen.Section.Mode.Create, False)

theSession.DeleteUndoMark(markId5, Nothing)

theSession.DeleteUndoMark(markId4, Nothing)

Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "section mark")

Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, Nothing)

Dim curves3(0) As NXOpen.IBaseCurve
Dim associativeLine3 As NXOpen.Features.AssociativeLine = CType(workPart.Features.FindObject("LINE(55)"), NXOpen.Features.AssociativeLine)

Dim line3 As NXOpen.Line = CType(associativeLine3.FindObject("CURVE 1"), NXOpen.Line)

curves3(0) = line3
Dim curveDumbRule3 As NXOpen.CurveDumbRule = Nothing
curveDumbRule3 = workPart.ScRuleFactory.CreateRuleBaseCurveDumb(curves3)

section1.AllowSelfIntersection(True)

Dim rules3(0) As NXOpen.SelectionIntentRule
rules3(0) = curveDumbRule3
Dim helpPoint3 As NXOpen.Point3d = New NXOpen.Point3d(90.774271177604206, 118.93303658225842, -4.2632564145606011e-14)
section1.AddToSection(rules3, line3, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint3, NXOpen.Section.Mode.Create, False)

theSession.DeleteUndoMark(markId7, Nothing)

Dim expression3 As NXOpen.Expression = Nothing
expression3 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit2)

theSession.DeleteUndoMark(markId6, Nothing)

Dim markId8 As NXOpen.Session.UndoMarkId = Nothing
markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude")

theSession.DeleteUndoMark(markId8, Nothing)

Dim markId9 As NXOpen.Session.UndoMarkId = Nothing
markId9 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude")

extrudeBuilder1.ParentFeatureInternal = False

Dim feature1 As NXOpen.Features.Feature = Nothing
feature1 = extrudeBuilder1.CommitFeature()

theSession.DeleteUndoMark(markId9, Nothing)

theSession.SetUndoMarkName(markId1, "Extrude")

Dim expression4 As NXOpen.Expression = extrudeBuilder1.Limits.StartExtend.Value

Dim expression5 As NXOpen.Expression = extrudeBuilder1.Limits.EndExtend.Value

extrudeBuilder1.Destroy()

workPart.Expressions.Delete(expression1)

workPart.Expressions.Delete(expression3)

workPart.Expressions.Delete(expression2)

' ----------------------------------------------
'   Menu: Tools->Journal->Stop Recording
' ----------------------------------------------

End Sub
End Module