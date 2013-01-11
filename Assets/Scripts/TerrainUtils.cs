using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// NOTE: hypothesis of size X = Z


public struct TerrainChunk {
	public Vector3 position;
	
	public TerrainChunk(Vector3 position){
		this.position = position;
	}
}

public class TerrainUtils : MonoBehaviour {
	 
	// Misc constants
 	public static int MAX_VAL = 10;
	public static int CHUNK_SIZE = 32; // Size in terrain units of chunks of same-value resources (must be 2^n)
	public static int TREE_MIN_LIMIT = (int)(0.8f*MAX_VAL); // Limit of resource for tree existence [0.1]
	public static float WATER_LEVEL = 10.0f;
	
	// Texture IDs
	static public int FIELD_TEXTURE_ID = 2;
	static public int SOWNFIELD_TEXTURE_ID = 6;
	static public int WETFIELD_TEXTURE_ID = 7;
	static public int ROAD_TEXTURE_ID = 3;
	
	// Options
	public bool redrawTerrain = false;
	public bool enabledTerrainDrawing = false;
	public bool centralizedTerrainDrawing = false;
	public bool repopulateTrees = false;
	public bool repopulateDetails = false;
	public bool recomputeHeights = false;
	public bool recomputeChunks = false;
		
	// Data
	private Terrain terrain;  
	private TerrainData terrainData;
	protected TerrainChunk[,] terrainChunks;
 	private float[,,] alphamap;
	
	// Sizes
	private Size size;
	private Size nChunks;

	// Highlights
	private Highlight[,] highlights;

	
	private struct Size{ 
		public int x;
		public int z;
		
		public Size(int x, int z){
			this.x = x;
			this.z = z;
		}
	}
		
		
	public void Awake(){
		this.terrain = Terrain.activeTerrain;
		this.terrainData = terrain.terrainData;
		 
		int terrainSize = 32*16;   
		int terrainHeight = 32;  
		terrainData.size = new Vector3(terrainSize,terrainHeight,terrainSize);
		terrainData.heightmapResolution = terrainSize/2+1;
		
		size = new Size((int)terrainData.size.x,(int)terrainData.size.z);
		nChunks = new Size((int)size.x/CHUNK_SIZE,(int)size.z/CHUNK_SIZE);
		 
		
		// Add highlights
		GameObject hlPrefab = Resources.Load("HighlightPrefab") as GameObject; 
		this.highlights = new Highlight[nChunks.x,nChunks.z];
		for (int chX = 0; chX < nChunks.x; chX++){
	    	for (int chZ = 0; chZ < nChunks.z; chZ++) { 
				this.highlights[chX,chZ] = (Instantiate(hlPrefab) as  GameObject).GetComponent<Highlight>();
				this.highlights[chX,chZ].transform.parent = transform;
				this.highlights[chX,chZ].name = "Highlight" + chX + chZ;
				this.highlights[chX,chZ].gameObject.active = false;
					
			}
		}
				
					
	}
	
	
	public void Start(){
		
		print("Terrain size: " + terrainData.size);
		print("HeightMap: " + terrainData.heightmapWidth + " * " + terrainData.heightmapHeight + " res: " + terrainData.heightmapResolution);
		print("AlphaMap: " + terrainData.alphamapWidth + " * " + terrainData.alphamapHeight + " res: " + terrainData.alphamapResolution);
		print("Details: " + terrainData.detailWidth + " * " + terrainData.detailHeight  + " res: " + terrainData.detailResolution);
		print("Chunks: " + nChunks.x + " * " + nChunks.z);
		
		
		doRedoTerrain();
	}
	
	
	private void doRedoTerrain(){
	
		// Note: heightmap has values in the range [0,1]
		float[,] heightmapData = terrainData.GetHeights(0,0,terrainData.heightmapWidth,terrainData.heightmapHeight);
		if (recomputeChunks) this.terrainChunks = new TerrainChunk[nChunks.x,nChunks.z];
	
		
		// Compute chunks			
		if (recomputeChunks) {
		 	for (int chX = 0; chX < nChunks.x; chX++){
		    	for (int chZ = 0; chZ < nChunks.z; chZ++) { 
					int x = chunkToWorldX(chX); // Midpoint 
					int z = chunkToWorldZ(chZ);
					
					terrainChunks[chX,chZ] = new TerrainChunk(new Vector3(x,0,z)); // With position
      
					// Compute height from 0.0 to 1.0
					float seed = 5235.25f;
					int nOctaves = 2;
					terrainChunks[chX,chZ].position.y = 0.5f+0.3f*SimplexNoise.noiseOctaves(nOctaves,(float)chX/(float)nChunks.x,(float)chZ/(float)nChunks.z,seed,1.0f);//.Range(1,10);	
					     
					// Scales noise, peak in the center, depends on X and Z
					float distanceFromCenter = Mathf.Sqrt( Mathf.Pow((float)chX/(float)nChunks.x -0.5f,2) + Mathf.Pow((float)chZ/(float)nChunks.z-0.5f,2));
					terrainChunks[chX,chZ].position.y += 0.5f*(1.0f-3.5f*distanceFromCenter*Mathf.Sqrt(0.5f)*2.0f); // Higher near the center of the map
	                                     
					if (terrainChunks[chX,chZ].position.y < 0) terrainChunks[chX,chZ].position.y = 0;
					if (terrainChunks[chX,chZ].position.y > 1) terrainChunks[chX,chZ].position.y = 1;  
					
					
				} 
			} 
		}

		
	 	// Iterate trough each terrain 'pixel'	 
		if (recomputeHeights) {    
		 	for (int x = 0; x < size.x; x++){
		    	for (int z = 0; z < size.z; z++) { 

					int heX = worldToHeightX(x);
					int heZ = worldToHeightZ(z);
					
					int chX = worldToChunkX(x);
					int chZ = worldToChunkZ(z);
					
					heightmapData[heX,heZ] = terrainChunks[chX,chZ].position.y;
					
				} 
			}
			
			terrainData.SetHeights(0,0,heightmapData);
		}
		
		
	 	for (int chX = 0; chX < nChunks.x; chX++){
	    	for (int chZ = 0; chZ < nChunks.z; chZ++) { 
				// Highlight				
				snapToChunk(this.highlights[chX,chZ].transform,terrainChunks[chX,chZ]);		
			}
		}

		
	}
	
	
	
	// Highlight
	public void highlightZone(Transform source, int range,  ActionType action){
		
		int chX = worldToChunkX (source.position.x);
		int chZ = worldToChunkZ (source.position.z);
		
		//highlights[chX,chZ].gameObject.active = true;
		
		Color color = Color.white;
		switch(action){
		case ActionType.ATTACK:
			color = new Color(1.0f,0.0f,0.0f,0.2f); break;
		case ActionType.MOVE:
			color = new Color(0.0f,0.0f,1.0f,0.2f); break;
		}
		
		for (int dx = -range; dx <= range; dx++){
			for (int dz = -range; dz <= range; dz++){
				if (chX+dx < 0 || chZ+dz < 0 || chX+dx >= nChunks.x || chZ+dz >= nChunks.z) continue;
				if (Mathf.Abs(dx) + Mathf.Abs (dz) <= range) {
					highlights[chX+dx,chZ+dz].setColor(color); // Same material	
					highlights[chX+dx,chZ+dz].gameObject.active = true;
					highlights[chX+dx,chZ+dz].setAction(action);
				}
			}
		}
		
	}
	
	public void resetHighlights(){
	 	for (int chX = 0; chX < nChunks.x; chX++){
	    	for (int chZ = 0; chZ < nChunks.z; chZ++) { 	
				highlights[chX,chZ].gameObject.active = false;
			} 
		}	
	}
	
	
	public float getSpike(float val, float center, float steepness){
		if (val <= center) return steepness*(val-center)+1;
		else return steepness*(-val+center)+1;		
	}
	
	 
	public Vector3 getNormal ( Vector3 pos  ){
		return terrainData.GetInterpolatedNormal(pos.x/terrainData.size.x,pos.z/terrainData.size.z);
	}    
	
	public float getSlope ( Vector3 pos  ){
		return terrainData.GetSteepness(pos.x/terrainData.size.x,pos.z/terrainData.size.z);
	}
	
	public float getHeight ( Vector3 pos  ){
		return terrain.SampleHeight(pos);  
	}
	
	public Vector3 getRandomTerrainPoint (){
		int randX = (int) (Random.value*terrainData.size.x);
		int randZ = (int) (Random.value*terrainData.size.z);
		Vector3 point = new Vector3(randX,0,randZ);
		point = new Vector3(point.x, getHeight(point), point.z);
		return point;
	}
	
	
	public bool isWater ( Vector3 pos  ){
		 return getHeight(pos) <= WATER_LEVEL;
	}
	 
	
	public void snapToChunk(Transform t){
		Vector3 pos = Vector3.zero;
		pos.x = worldToChunkX(t.position.x)*CHUNK_SIZE + CHUNK_SIZE/2;
		pos.z = worldToChunkZ(t.position.z)*CHUNK_SIZE + CHUNK_SIZE/2;
		pos.y = getHeight(pos);
		t.position = pos;
	}
	
	
	public void snapToChunk(Transform t, TerrainChunk chunk){
		Vector3 pos = Vector3.zero;
		pos.x = worldToChunkX(chunk.position.x)*CHUNK_SIZE + CHUNK_SIZE/2;
		pos.z = worldToChunkZ(chunk.position.z)*CHUNK_SIZE + CHUNK_SIZE/2;
		pos.y = getHeight(pos);
		t.position = pos;
	}
	
					
	
	private int worldToHeightX(float worldX){	return (int)(worldX)*terrainData.heightmapResolution/size.x;}
	private int worldToHeightZ(float worldZ){	return (int)(worldZ)*terrainData.heightmapResolution/size.z;} 
	
	private int worldToAlphaX(float worldX){	return (int)(worldX)*terrainData.alphamapResolution/size.x;} // TODO needs a +1? or a Floor?
	private int worldToAlphaZ(float worldZ){	return (int)(worldZ)*terrainData.alphamapResolution/size.z;} 
		
	private int worldToDetailX(float worldX){	return (int)(worldX)*terrainData.detailResolution/size.x;} 
	private int worldToDetailZ(float worldZ){	return (int)(worldZ)*terrainData.detailResolution/size.z;} 
	
	private int offset = 0;//CHUNK_SIZE/2;
	private int worldToChunkX(float worldX){return (int)(worldX - offset) / CHUNK_SIZE; }
	private int worldToChunkZ(float worldZ){return (int)(worldZ - offset)  / CHUNK_SIZE; }
	  
	private int chunkToWorldX(int chunkX){ return (chunkX*CHUNK_SIZE+offset);}
	private int chunkToWorldZ(int chunkZ){ return (chunkZ*CHUNK_SIZE+offset);}
	 
 
}