-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le :  mer. 16 août 2017 à 15:04
-- Version du serveur :  10.1.25-MariaDB
-- Version de PHP :  7.1.7




/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  dbprologm2
--

-- --------------------------------------------------------

--
-- Structure de la table bio
--
USE  [dbprologm2]
GO
CREATE TABLE bio (
  enfant varchar(255) NOT NULL,
  sexe varchar(255) NOT NULL,
  annee_naissance varchar(255) NOT NULL,
  annee_mort varchar(255) NOT NULL,
  pere varchar(255) NOT NULL,
  mere varchar(255) NOT NULL,
  id INT NOT NULL
) 

--
-- Déchargement des données de la table bio
--

INSERT INTO bio (enfant, sexe, annee_naissance, annee_mort, pere, mere, id) VALUES
('louis13', 'h', '1601', '1643', 'henri4', 'marie_medicis', 1),
('elisabeth_France', 'f', '1603', '1644', 'henri4', 'marie_medicis', 3),
('marie_therese_Autriche', 'f', '1638', '1683', 'philippe4', 'elisabeth_france', 4),
('louis14', 'h', '1638', '1715', 'louis13', 'anne_autriche', 5),
('grand_dauphin', 'h', '1661', '1711', 'louis14', 'marie_therese_autriche', 6),
('louis_bourbon', 'h', '1682,', '1712', 'grand_dauphin', 'marie_anne_baviere', 7),
('philippe5', 'h', '1683', '1746', 'grand_dauphin', 'marie_anne_baviere', 8),
('louis15', 'h', '1710', '1774', 'louis_bourbon', 'marie_adelaide_savoie', 9),
('louis_dauphin', 'h', '1729', '1765', 'louis15', 'marie_leczcynska', 10),
('louis16', 'h', '1754', '1793', 'louis_dauphin', 'marie_josephe_saxe', 11),
('louis18', 'h', '1755', '1824', 'louis_dauphin', 'marie_josephe_saxe', 12),
('charles10', 'h', '1757', '1836', 'louis_dauphin', 'marie_josephe_saxe', 13),
('clotilde', 'f', '1759', '1802', 'louis_dauphin', 'marie_josephe_saxe', 14),
('louis17', 'h', '1785', '1795', 'louis16', 'marie_antoinette', 15),
('philippe1', 'h', '1640', '1701', 'louis13', 'anne_autriche', 16),
('philippe2', 'h', '1674', '1723', 'philippe1', 'charlotte_baviere', 17),
('louis_orleans', 'h', '1703', '1752', 'philippe', 'francoise_marie_bourbon', 18),
('louis_philippe', 'h', '1725', '1785', 'louis_orleans', 'augusta_marie_bade', 19),
('philippe_egalite', 'h', '1747', '1793', 'louis_philippe', 'louise_henriette_bourbon_conti', 20),
('louis_philippe1', 'h', '1773', '1850', 'philippe_egalite', 'louise_marie_adelaide_bourbon_penthievre', 21);

--
-- Index pour les tables déchargées
--

--
-- Index pour la table bio
--
ALTER TABLE bio
  ADD PRIMARY KEY (id);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table bio
--


/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
