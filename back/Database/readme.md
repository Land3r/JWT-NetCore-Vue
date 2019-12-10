# Installation

Pour déployer une version de base de données, se connecter au serveur sur lequel le service mongodb tourne.  
Transferer les fichiers de la version sur le serveur.  
Executer les scipts un à un, à l'aide de la commande `mongo /fullpath/to/script/file.js`

Notez que les scripts sont commulatifs, c'est à dire que pour installer la version 1.3, la version 1.0 doit être deployé puis la version 1.1 puis 1.2 et enfin 1.3.

Notez aussi que les mots de passe sont hashés en base de données, avec un salt définit dans le fichier appsettings.json.
Les mots de passe stockés dans ces scripts de base de données utilisent le salt définit par défaut dans le fichier de config, c'est à dire: `dBzIRLUldJ3xd6RvNWV4`