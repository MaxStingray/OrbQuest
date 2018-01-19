using UnityEngine;
using System.Collections;
using ooparts.dungen;

namespace ooparts.dungen
{
    /// <summary>
    /// strictly for generating map and listening for triggers and flags
    /// </summary>
	public class RoomMapManager : MonoBehaviour
	{
        static public bool generated;
        static public bool restarted;
        public static int _numFloors;
        public static int _currFloor;


        public Stairs stairs;

		public Map mapPrefap;
		private Map mapInstance;

		public int MapSizeX;
		public int MapSizeZ;
		public int MaxRooms;
		public int MinRoomSize;
		public int MaxRoomSize;
        public int numberOfFloors;
        public int currentFloor;

		public int TileSizeFactor = 1;
		public static int TileSize;

        public DungeonParameters DP;

        public bool bossFloor = false;
        
        public bool bossDead = false;
        GameObject bossObj;

        void Awake()
        {
            if (DP == null)
            {
                DP = GameObject.FindGameObjectWithTag("parametersObject").GetComponent<DungeonParameters>();
                if (DP != null)
                {
                    MapSizeX = DP.sizeX;
                    MapSizeZ = DP.sizeZ;
                    MaxRooms = DP.maxRooms;
                    numberOfFloors = DP.numFloors;
                }
            }
            else
            {
                Debug.Log("already found parameters");
            }

            _numFloors = numberOfFloors;
        }

		void Start()
		{
            currentFloor = 1;
			TileSize = TileSizeFactor;
			BeginGame();
		}

		void Update()
		{
            _currFloor = currentFloor;

            

            if (stairs == null)
            {
                stairs = GameObject.FindObjectOfType<Stairs>();
            }
            else
            {
                if (stairs.reachedStairs)
                {
                    currentFloor++;
                    RestartGame();
                }
            }


		}

		private void BeginGame()
		{           
			mapInstance = Instantiate(mapPrefap);
			mapInstance.RoomCount = MaxRooms;
			mapInstance.MapSize = new IntVector2(MapSizeX, MapSizeZ);
			mapInstance.RoomSize.Min = MinRoomSize;
			mapInstance.RoomSize.Max = MaxRoomSize;
			TileSize = TileSizeFactor;
           
            StartCoroutine(mapInstance.Generate());
		}

		private void RestartGame()
		{
            generated = false;
            restarted = true;
			StopAllCoroutines();
			Destroy(mapInstance.gameObject);
            DestroyPickups();
			BeginGame();
		}


        private void DestroyPickups()
        {
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
            if (pickups != null)
            {
                for (int i = 0; i < pickups.Length; i++)
                {
                    Destroy(pickups[i]);
                }
            }
            
        }
        
	}
}