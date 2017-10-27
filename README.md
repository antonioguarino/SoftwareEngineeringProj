#Uso di github
1 - Scaricate il client di github dal sito, registratevi e datemi la mail cosi posso aggiungervi come contributor e potete caricare le modifiche del progetto
2 - Create una cartella da qualche parte. Conterrà i file che sono nel repository
3 - Aprite Github shell e arrivate con "cd" nella cartella che avete creato
4 - Nella shell digitate "git init"
5 - Nella shell eseguite "git remote add origin https://github.com/antonioguarino/SoftwareEngineeringProj"
6 - Eseguire "git pull" per mettervi in locale tutti i file che sono attualmente sul repository
7 - Modificate il progetto
8 - Eseguire "git add *" per rimettere tutti i file (includendo le modifiche) sul repository
9 - Eseguire "git commit -m "Commento sulla modifica fatta" " 
10 - Eseguire "git push"


I passi da 6 a 10 vanno ripetuti ogni volta che si lavora al progetto
Sostanzialmente dovete prendere i file (pull) aggiungere ai file da trasferire quelli che avete modificato (add e commit) ed uploadarli (push)

# Starter Project
---

*Copyright (C) 2017 Improbable Worlds Limited. All rights reserved.*

- *GitHub repository*: [https://github.com/spatialos/StarterProject](https://github.com/spatialos/StarterProject)

---

## Introduction

This is a SpatialOS starter project with useful core features that you can extend to build your own SpatialOS application.

It contains:

* A Player spawned on client connection as per the [Unity Client Lifecycle Guide](https://spatialos.improbable.io/docs/reference/latest/tutorials/unity-client-lifecycle).
* A Cube spawned through a snapshot via an entity template method and an Unity prefab.
* The rest of the features included in the [BlankProject](https://github.com/spatialos/BlankProject).

If you run into problems, or want to give us feedback, please visit the [SpatialOS forums](https://forums.improbable.io/).

## Running the project

To run the project locally, first build it by running `spatial worker build`, then start the server with `spatial local start`. You can connect a client by opening the Unity project and pressing the play button, or by running `spatial local worker launch UnityClient default`. See the [documentation](https://spatialos.improbable.io/docs/reference/latest/developing/local/run) for more details.

To deploy the project to the cloud, first build it by running `spatial worker build -t=deployment`, then upload the assembly with `spatial cloud upload <assembly name>`, and finally deploy it with `spatial cloud launch <assembly name> <launch configuration file> <deployment name> --snapshot=<snapshot file>`. You can obtain and share links to connect to the deployment from the [console](http://console.improbable.io/projects). See the [documentation](https://docs.improbable.io/reference/latest/developing/deploy-cloud) for more details.
