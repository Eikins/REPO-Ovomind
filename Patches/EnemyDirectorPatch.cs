using HarmonyLib;
using Ovomind;
using System;
using System.Collections;
using UnityEngine;

namespace OvomindEmotions.Patches
{
    [HarmonyPatch(typeof(EnemyDirector))]
    internal class EnemyDirectorPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(EnemyDirector.Start))]
        static void StartCoroutine(EnemyDirector __instance)
        {
            __instance.StartCoroutine(TrackEnemies(__instance));
            __instance.StartCoroutine(TrackEmotion());
        }

        private static IEnumerator TrackEnemies(EnemyDirector enemyDirector)
        {
            Plugin.Logger.LogInfo($"Start enemy tracking...");

            while (true)
            {
                yield return new WaitForSeconds(20f);
                if (enemyDirector.enemiesSpawned.Count == 0)
                {
                    Plugin.Logger.LogInfo("No ennemy yet.");
                }
                else
                {
                    Plugin.Logger.LogInfo("-----------------------");
                    Plugin.Logger.LogInfo("Listing ennemies:");
                    foreach (var enemy in enemyDirector.enemiesSpawned)
                    {
                        Plugin.Logger.LogInfo($"{enemy.name}");
                        Plugin.Logger.LogInfo($"- Spawns in: {enemy.DespawnedTimer}");
                        Plugin.Logger.LogInfo($"- Difficulty: {enemy.difficulty}");
                    }
                    Plugin.Logger.LogInfo("-----------------------");
                }
            }
        }

        private static IEnumerator TrackEmotion()
        {
            var emotions = (Emotion[]) Enum.GetValues(typeof(Emotion));
            Plugin.Logger.LogInfo($"Emotions: [{string.Join(", ", emotions)}]");
            int emotionIndex = 0;
            while (true)
            {
                emotionIndex = UnityEngine.Random.RandomRangeInt(0, emotions.Length);
                Plugin.Logger.LogInfo($"CurrentEmotion: {emotions[emotionIndex]}");
                var postProcessing = PostProcessing.Instance;
                if (postProcessing != null)
                {
                    postProcessing.vignetteColor = emotions[emotionIndex].GetColor();
                    postProcessing.vignetteIntensity = 0.7f;
                }

                yield return new WaitForSeconds(4f);
            }
        }
    }
}
