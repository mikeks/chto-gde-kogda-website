Imports Microsoft.VisualBasic

Public Class DataReceiverManager

    Private Shared Function GetTeamId(teamName As String) As Integer
        Try
            Return Team.All.First(Function(t) t.Value.Name.ToLowerInvariant = teamName.ToLowerInvariant).Key
        Catch ex As Exception
            Throw New Exception("Команда не найдена: " & teamName)
        End Try
    End Function

    Private Shared Sub SaveTeamResult(gameNumber As Integer, dt As TeamData)

        Dim teamId = GetTeamId(dt.TeamName)

        DbObject.ExecStoredProc("SaveGameResult",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@gameNum", gameNumber)
                cmd.Parameters.AddWithValue("@teamId", teamId)
                cmd.Parameters.AddWithValue("@points", dt.AnsweredQuestions.Trim(",").Split(",").Length)
                cmd.Parameters.AddWithValue("@answeredQuestions", dt.AnsweredQuestions)
            End Sub)

    End Sub

    Private Shared Sub SaveGameQuestionTeamsResults(gameNumber As Integer, questionNumber As Integer, teamsAnswered As String, totalTeamCount As Integer)

        DbObject.ExecStoredProc("SaveGameQuestionTeamsResults",
            Sub(cmd)
                cmd.Parameters.AddWithValue("@gameNum", gameNumber)
                cmd.Parameters.AddWithValue("@questionNum", questionNumber)
                cmd.Parameters.AddWithValue("@teamsAnswered", teamsAnswered)
                cmd.Parameters.AddWithValue("@teamsPlayedCount", totalTeamCount)
            End Sub)

    End Sub


    Public Shared Sub ProcessData(dt As TransferData)

        Dim quest As New Dictionary(Of Integer, String) ' question num, list of team ids (comma separated)

        For Each tm In dt.Data
            SaveTeamResult(dt.GameNumber, tm)

            Dim teamId = GetTeamId(tm.TeamName)

            For Each aq In tm.AnsweredQuestions.Split(",")
				If (String.IsNullOrWhiteSpace(aq)) Then Continue For
				Dim questNum = Integer.Parse(aq)
                If (quest.ContainsKey(questNum)) Then
                    quest(questNum) &= "," & teamId
                Else
                    quest.Add(questNum, teamId)
                End If

            Next

        Next

        For Each qu In quest

            Dim questNum = qu.Key
            Dim TeamIdList = qu.Value

            SaveGameQuestionTeamsResults(dt.GameNumber, questNum, TeamIdList, dt.Data.Length)

        Next


    End Sub


End Class
