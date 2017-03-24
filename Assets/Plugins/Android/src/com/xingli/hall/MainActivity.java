package com.xingli.hall;

import android.app.Activity;

import android.content.Intent;
import android.os.Bundle;


public class MainActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		
	}
	public void startFish()
	{ 
		Intent intent = this.getPackageManager().getLaunchIntentForPackage("com.xingli.overfish");
		  if(intent!=null)
		  {
		    startActivity(intent);
		  }
			
			 
		
		
	}
}
