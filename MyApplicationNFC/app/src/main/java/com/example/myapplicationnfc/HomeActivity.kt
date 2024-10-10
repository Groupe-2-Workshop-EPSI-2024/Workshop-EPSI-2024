package com.example.myapplicationnfc

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity

class HomeActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home)

        val welcomeTextView: TextView = findViewById(R.id.welcomeTextView)
        welcomeTextView.text = "Bonjour! Vous êtes maintenant connecté."

        // Ajout de la logique pour le bouton
        val viewAddictionsButton: Button = findViewById(R.id.viewAddictionsButton)
        viewAddictionsButton.setOnClickListener {
            // Démarrer l'activité AddictionListActivity
            val intent = Intent(this, AddictionListActivity::class.java)
            startActivity(intent)
        }
    }
}
