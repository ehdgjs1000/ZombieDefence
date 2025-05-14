using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BackEnd;

public class BackEndGameData
{
    [System.Serializable]
    public class GameDataLoadEvent : UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private static BackEndGameData instance = null;
    public static BackEndGameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackEndGameData();
            }
            return instance;
        }
    }

    private UserGameData userGameData = new UserGameData();
    public UserGameData UserGameData => userGameData;

    private UserQuestData userQuestData = new UserQuestData();
    public UserQuestData UserQuestData => userQuestData; 

    private string gameDataRowInData = string.Empty;
    private string gameQuestDataRowInData = string.Empty;

    /// <summary>
    /// 뒤끝 콘솔 테이블에 새로운 유저 정보 추가
    /// </summary>
    public void GameDataInsert()
    {
        //유저 정보를 초기값으로 설정
        userGameData.Reset();
        userQuestData.Reset();

        // 테이블에 추가할 데이터로 가공
        Param param = new Param()
        {
            {"level", userGameData.level},
            {"exp", userGameData.exp },
            {"gold", userGameData.gold},
            {"crystal", userGameData.crystal },
            { "energy", userGameData.energy },
            { "promotion", userGameData.promotionType}
        };

        Param questParam = new Param()
        {
            {"Q1Amount", UserQuestData.questCount[0] },
            {"Q2Amount", UserQuestData.questCount[1] },
            {"Q3Amount", UserQuestData.questCount[2] },
            {"Q4Amount", UserQuestData.questCount[3] },
            {"Q5Amount", UserQuestData.questCount[4] },
            {"Q6Amount", UserQuestData.questCount[5] },
            {"Q1Received", UserQuestData.isReceived[0] },
            {"Q2Received", UserQuestData.isReceived[1] },
            {"Q3Received", UserQuestData.isReceived[2] },
            {"Q4Received", UserQuestData.isReceived[3] },
            {"Q5Received", UserQuestData.isReceived[4] },
            {"Q6Received", UserQuestData.isReceived[5] },
            {"Q7Received", UserQuestData.isReceived[6] },
            {"R1Received", UserQuestData.isRewardReceived[0]},
            {"R2Received", UserQuestData.isRewardReceived[1]},
            {"R3Received", UserQuestData.isRewardReceived[2]},
            {"R4Received", UserQuestData.isRewardReceived[3]},
            {"R5Received", UserQuestData.isRewardReceived[4]},
            {"questClearAmount", UserQuestData.questClearAmount }
        };

        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            if (callback.IsSuccess())
            {
                gameDataRowInData = callback.GetInDate();
                Debug.Log($"게임 정보 데이터 삽입에 성공했습니다. : {callback}");
            }
            else //실패시
            {
                Debug.LogError($"게임 정보 데이터 삽입에 실패했습니다 : {callback}");
            }
        });
        Backend.GameData.Insert("USER_QUESTDATA", questParam, callback =>
        {
            if (callback.IsSuccess())
            {
                gameQuestDataRowInData = callback.GetInDate();
                Debug.Log($"퀘스트 정보 데이터 삽입에 성공했습니다. : {callback}");
            }
            else
            {
                Debug.LogError($"퀘스트 정보 데이터 삽입에 실패했습니다 : {callback}");
            }
        });

    }

    /// <summary>
    /// 뒤끝 콘솔 테이블에서 유저 정보를 불러올 떄 호출
    /// </summary>
    public void GameDataLoad()
    {
        Debug.Log("Load Server");

        Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                Debug.Log($"게임 정보 데이터 불러오기에 성공했습니다. : {callback}");

                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("데이터가 존재하지 않습니다.");
                    }
                    else
                    {
                        //불러온 게임 정보의 고유 값
                        gameDataRowInData = gameDataJson[0]["inDate"].ToString();
                        //불러온 게임 정보를 userGameData 변수에 저장
                        userGameData.level = int.Parse(gameDataJson[0]["level"].ToString());
                        userGameData.exp = int.Parse(gameDataJson[0]["exp"].ToString());
                        userGameData.gold = int.Parse(gameDataJson[0]["gold"].ToString());
                        userGameData.crystal = int.Parse(gameDataJson[0]["crystal"].ToString());
                        userGameData.energy = int.Parse(gameDataJson[0]["energy"].ToString());
                        userGameData.promotionType = int.Parse(gameDataJson[0]["promotion"].ToString());

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                catch (System.Exception e)
                {
                    userGameData.Reset();
                    userQuestData.Reset();

                    Debug.LogError(e);
                }
            }
            else // 실패했을 때
            {
                Debug.LogError($"게임 정보 데이터 불러오기에 실패했습니다. : {callback}");
            }

        });
        Backend.GameData.GetMyData("USER_QUESTDATA", new Where(), callback =>
        {
            if (callback.IsSuccess())
            {
                Debug.Log($"게임 정보 데이터 불러오기에 성공했습니다. : {callback}");

                try
                {
                    LitJson.JsonData gameQuestDataJson = callback.FlattenRows();

                    if (gameQuestDataJson.Count <= 0)
                    {
                        Debug.LogWarning("데이터가 존재하지 않습니다.");
                    }
                    else
                    {
                        //불러온 게임 정보의 고유 값
                        gameQuestDataRowInData = gameQuestDataJson[0]["inDate"].ToString();
                        //불러온 게임 정보를 userGameData 변수에 저장
                        userQuestData.questCount[0] = int.Parse(gameQuestDataJson[0]["Q1Amount"].ToString());
                        userQuestData.questCount[1] = int.Parse(gameQuestDataJson[0]["Q2Amount"].ToString());
                        userQuestData.questCount[2] = int.Parse(gameQuestDataJson[0]["Q3Amount"].ToString());
                        userQuestData.questCount[3] = int.Parse(gameQuestDataJson[0]["Q4Amount"].ToString());
                        userQuestData.questCount[4] = int.Parse(gameQuestDataJson[0]["Q5Amount"].ToString());
                        userQuestData.questCount[5] = int.Parse(gameQuestDataJson[0]["Q6Amount"].ToString());
                        userQuestData.isReceived[0] = bool.Parse(gameQuestDataJson[0]["Q1Received"].ToString());
                        userQuestData.isReceived[1] = bool.Parse(gameQuestDataJson[0]["Q2Received"].ToString());
                        userQuestData.isReceived[2] = bool.Parse(gameQuestDataJson[0]["Q3Received"].ToString());
                        userQuestData.isReceived[3] = bool.Parse(gameQuestDataJson[0]["Q4Received"].ToString());
                        userQuestData.isReceived[4] = bool.Parse(gameQuestDataJson[0]["Q5Received"].ToString());
                        userQuestData.isReceived[5] = bool.Parse(gameQuestDataJson[0]["Q6Received"].ToString());
                        userQuestData.isReceived[6] = bool.Parse(gameQuestDataJson[0]["Q7Received"].ToString());
                        userQuestData.isRewardReceived[0] = bool.Parse(gameQuestDataJson[0]["R1Received"].ToString());
                        userQuestData.isRewardReceived[1] = bool.Parse(gameQuestDataJson[0]["R2Received"].ToString());
                        userQuestData.isRewardReceived[2] = bool.Parse(gameQuestDataJson[0]["R3Received"].ToString());
                        userQuestData.isRewardReceived[3] = bool.Parse(gameQuestDataJson[0]["R4Received"].ToString());
                        userQuestData.isRewardReceived[4] = bool.Parse(gameQuestDataJson[0]["R5Received"].ToString());
                        userQuestData.questClearAmount = float.Parse(gameQuestDataJson[0]["questClearAmount"].ToString());

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                catch (System.Exception e)
                {
                    userGameData.Reset();
                    userQuestData.Reset();

                    Debug.LogError(e);
                }
            }
            else // 실패했을 때
            {
                Debug.LogError($"게임 정보 데이터 불러오기에 실패했습니다. : {callback}");
            }

        });
    }

    /// <summary>
    /// 뒤끝 콘솔 테이블에 있는 유저 데이터 갱신
    /// </summary>
    public void GameDataUpdate(UnityAction action)
    {
        if (userGameData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. + " +
                "Insert 혹은 Load를 통해 데이터를 생성해주세요.");
            return;
        } else if (userQuestData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. + " +
                "Insert 혹은 Load를 통해 데이터를 생성해주세요.");
            return;
        }

        Param param = new Param()
        {
            {"level", userGameData.level},
            {"exp", userGameData.exp },
            {"gold", userGameData.gold},
            {"crystal", userGameData.crystal },
            {"energy", userGameData.energy},
            {"promotion", userGameData.promotionType }
        };
        Param questParam = new Param()
        {
            {"Q1Amount", UserQuestData.questCount[0] },
            {"Q2Amount", UserQuestData.questCount[1] },
            {"Q3Amount", UserQuestData.questCount[2] },
            {"Q4Amount", UserQuestData.questCount[3] },
            {"Q5Amount", UserQuestData.questCount[4] },
            {"Q6Amount", UserQuestData.questCount[5] },
            {"Q1Received", UserQuestData.isReceived[0] },
            {"Q2Received", UserQuestData.isReceived[1] },
            {"Q3Received", UserQuestData.isReceived[2] },
            {"Q4Received", UserQuestData.isReceived[3] },
            {"Q5Received", UserQuestData.isReceived[4] },
            {"Q6Received", UserQuestData.isReceived[5] },
            {"Q7Received", UserQuestData.isReceived[6] },
            {"R1Received", userQuestData.isRewardReceived[0] },
            {"R2Received", userQuestData.isRewardReceived[1] },
            {"R3Received", userQuestData.isRewardReceived[2] },
            {"R4Received", userQuestData.isRewardReceived[3] },
            {"R5Received", userQuestData.isRewardReceived[4] },
            {"questClearAmount", userQuestData.questClearAmount }
        };

        //게임 정보의 고유값(gameDataRowInDate)가 없으면 에러 메세지 출력
        if (string.IsNullOrEmpty(gameDataRowInData))
        {
            Debug.LogError("유저의 inDate정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
        }
        //게임 정보의 고유값이 있으면 테이블에 저장되어 있는 값 중 inDate 컬럼의 값과
        //소유하는 유저의 owner_inDate가 일치하는 row를 검색하여 수정하는 UpdateV2()를 호출
        else
        {
            Debug.Log($"{gameDataRowInData}의 게임정보 데이터 수정을 요청합니다.");

            Backend.GameData.UpdateV2("USER_DATA", gameDataRowInData, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");

                    action?.Invoke();
                }
                else
                {
                    Debug.Log($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
                }

            });

        }
        if (string.IsNullOrEmpty(gameQuestDataRowInData))
        {
            Debug.LogError("유저의 inDate정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
        }
        //게임 정보의 고유값이 있으면 테이블에 저장되어 있는 값 중 inDate 컬럼의 값과
        //소유하는 유저의 owner_inDate가 일치하는 row를 검색하여 수정하는 UpdateV2()를 호출
        else
        {
            Debug.Log($"{gameQuestDataRowInData}의 게임정보 데이터 수정을 요청합니다.");

            Backend.GameData.UpdateV2("USER_QUESTDATA", gameQuestDataRowInData, Backend.UserInDate, questParam, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");

                    action?.Invoke();
                }
                else
                {
                    Debug.Log($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
                }

            });

        }
    }
    public void GameDataUpdate()
    {
        Debug.Log("Update Server");
        if (userGameData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. + " +
                "Insert 혹은 Load를 통해 데이터를 생성해주세요.");
            return;
        }else if (userQuestData == null)
        {
            return;
        }

        Param param = new Param()
        {
            {"level", userGameData.level},
            {"exp", userGameData.exp },
            {"gold", userGameData.gold},
            {"crystal", userGameData.crystal },
            {"energy", userGameData.energy},
            {"promotion", userGameData.promotionType }
        };
        Param questParam = new Param()
        {
            {"Q1Amount", UserQuestData.questCount[0] },
            {"Q2Amount", UserQuestData.questCount[1] },
            {"Q3Amount", UserQuestData.questCount[2] },
            {"Q4Amount", UserQuestData.questCount[3] },
            {"Q5Amount", UserQuestData.questCount[4] },
            {"Q6Amount", UserQuestData.questCount[5] },
            {"Q1Received", UserQuestData.isReceived[0] },
            {"Q2Received", UserQuestData.isReceived[1] },
            {"Q3Received", UserQuestData.isReceived[2] },
            {"Q4Received", UserQuestData.isReceived[3] },
            {"Q5Received", UserQuestData.isReceived[4] },
            {"Q6Received", UserQuestData.isReceived[5] },
            {"Q7Received", UserQuestData.isReceived[6] },
            {"R1Received", userQuestData.isRewardReceived[0] },
            {"R2Received", userQuestData.isRewardReceived[1] },
            {"R3Received", userQuestData.isRewardReceived[2] },
            {"R4Received", userQuestData.isRewardReceived[3] },
            {"R5Received", userQuestData.isRewardReceived[4] },
            {"questClearAmount", userQuestData.questClearAmount}
        };

        //게임 정보의 고유값(gameDataRowInDate)가 없으면 에러 메세지 출력
        if (string.IsNullOrEmpty(gameDataRowInData))
        {
            //Debug.LogError("유저의 inDate정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
        }
        //게임 정보의 고유값이 있으면 테이블에 저장되어 있는 값 중 inDate 컬럼의 값과
        //소유하는 유저의 owner_inDate가 일치하는 row를 검색하여 수정하는 UpdateV2()를 호출
        else
        {
            Debug.Log($"{gameDataRowInData}의 게임정보 데이터 수정을 요청합니다.");

            Backend.GameData.UpdateV2("USER_DATA", gameDataRowInData, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");
                }
                else
                {
                    Debug.Log($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
                }

            });
        }

        if (string.IsNullOrEmpty(gameQuestDataRowInData))
        {
            Debug.Log(gameQuestDataRowInData);
            Debug.LogError("유저의 inDate정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
        }
        //게임 정보의 고유값이 있으면 테이블에 저장되어 있는 값 중 inDate 컬럼의 값과
        //소유하는 유저의 owner_inDate가 일치하는 row를 검색하여 수정하는 UpdateV2()를 호출
        else
        {
            Debug.Log($"{gameQuestDataRowInData}의 게임정보 데이터 수정을 요청합니다.");

            Backend.GameData.UpdateV2("USER_QUESTDATA", gameQuestDataRowInData, Backend.UserInDate, questParam, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");
                }
                else
                {
                    Debug.Log($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
                }

            });

        }
    }
    public void GameDataReset()
    {
        if (userGameData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. + " +
                "Insert 혹은 Load를 통해 데이터를 생성해주세요.");
            return;
        } else if (userQuestData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. + " +
                "Insert 혹은 Load를 통해 데이터를 생성해주세요.");
            return;
        }

        //게임 정보의 고유값(gameDataRowInDate)가 없으면 에러 메세지 출력
        if (string.IsNullOrEmpty(gameDataRowInData))
        {
            Debug.LogError("유저의 inDate정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
        }
        //게임 정보의 고유값이 있으면 테이블에 저장되어 있는 값 중 inDate 컬럼의 값과
        //소유하는 유저의 owner_inDate가 일치하는 row를 검색하여 수정하는 UpdateV2()를 호출
        else
        {
            Debug.Log($"{gameDataRowInData}의 게임정보 데이터 삭제을 요청합니다.");

            Backend.GameData.DeleteV2("USER_DATA", gameDataRowInData, Backend.UserInDate, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");
                }
                else
                {
                    Debug.Log($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
                }

            });
        }

        if (string.IsNullOrEmpty(gameQuestDataRowInData))
        {
            Debug.LogError("유저의 inDate정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
        }
        //게임 정보의 고유값이 있으면 테이블에 저장되어 있는 값 중 inDate 컬럼의 값과
        //소유하는 유저의 owner_inDate가 일치하는 row를 검색하여 수정하는 UpdateV2()를 호출
        else
        {
            Debug.Log($"{gameQuestDataRowInData}의 게임정보 데이터 삭제을 요청합니다.");

            Backend.GameData.DeleteV2("USER_QUESTDATA", gameQuestDataRowInData, Backend.UserInDate, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");
                }
                else
                {
                    Debug.Log($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
                }

            });
        }
    }
}
