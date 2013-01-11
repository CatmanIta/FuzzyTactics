using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Presents a menu with different repositories of FuzzyGUIElements

public class FuzzyGUIRepoMenu : Menu {
	
	protected List<FuzzyGUIRepository> repos = new List<FuzzyGUIRepository>();
	//protected List<string> labels = new List<string>();
	
	private FuzzyGUIRepository selectedRepo;
	
	public void Awake(){
		//base.Awake();
		
		// Big text at the top
		//textMesh.transform.localPosition = new Vector3(-20,30,0);
		//textMesh.characterSize = 0;
		
		
		// Add repositories		
		addRepo("RepositorySubjects");
		addRepo("RepositoryAdjectives");
		addRepo("RepositoryProperties");
		
		
		//updateRepos();
	}
	
	public void Start(){
		//base.Start();
		selectRepo(repos[0]);
	}
	
	//private void updateRepos(){}
	
	public void showGUI(Rect area){
		
		// TODO: don't use GUI Layout, do it yourself!
		// Show what repositories are available
		
		float titlesH = 0.15f;
		beginGUILayer(0,0,0,area.width,area.height*titlesH);
			float buttonWidth = cw[0]*0.1f;
			for(int i=0; i<repos.Count; i++){
				if (GUI.Button(new Rect(0+buttonWidth*i,0,buttonWidth,ch[0]),repos[i].vName)) selectRepo(repos[i]);				
			}
		endGUILayer();
		
		// Show the selected repository
		area = new Rect(area.x,area.y+area.height*titlesH,area.width,area.height*(1-titlesH));
		selectedRepo.showGUI(area);
	}
	
	private void selectRepo(FuzzyGUIRepository repo){
		// Show the contents of the selected repository
		if (selectedRepo) selectedRepo.hide();
		selectedRepo = repo;
		
	}
	
	private void addRepo(string repoName){
		GameObject repoGo = new GameObject(repoName);
		repoGo.transform.parent = transform;
		repoGo.transform.position = transform.position;
		
		FuzzyGUIRepository repo = (FuzzyGUIRepository)repoGo.AddComponent(repoName);
		
		this.repos.Add(repo);
		repo.hide();
		
	}
	
	
	
}
