-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 11, 2025 at 12:01 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `panabosaging`
--

-- --------------------------------------------------------

--
-- Table structure for table `fare_rates`
--

CREATE TABLE `fare_rates` (
  `base_fare` varchar(50) NOT NULL,
  `pkm_fare` varchar(50) NOT NULL,
  `wholeride_fare` varchar(50) NOT NULL,
  `OFR_fare` varchar(50) NOT NULL,
  `holiday_fare` varchar(50) NOT NULL,
  `discounted_fare` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `fare_rates`
--

INSERT INTO `fare_rates` (`base_fare`, `pkm_fare`, `wholeride_fare`, `OFR_fare`, `holiday_fare`, `discounted_fare`) VALUES
('10', '2', '50', '80', '12', '8');

-- --------------------------------------------------------

--
-- Table structure for table `login`
--

CREATE TABLE `login` (
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `login`
--

INSERT INTO `login` (`username`, `password`) VALUES
('rhaiah', 'mapua'),
('ryu', 'grah1234');

-- --------------------------------------------------------

--
-- Table structure for table `vehicle_drivers`
--

CREATE TABLE `vehicle_drivers` (
  `plateNo` int(50) NOT NULL,
  `bodyNo` int(50) NOT NULL,
  `driverName` varchar(50) NOT NULL,
  `licenseNo` int(50) NOT NULL,
  `crNo` int(50) NOT NULL,
  `vehicleModel` varchar(50) NOT NULL,
  `status` varchar(50) NOT NULL,
  `permitExpiry` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `vehicle_drivers`
--

INSERT INTO `vehicle_drivers` (`plateNo`, `bodyNo`, `driverName`, `licenseNo`, `crNo`, `vehicleModel`, `status`, `permitExpiry`) VALUES
(123456789, 963852741, 'Gina Cole', 741852963, 852147963, 'Honda TMX', 'Active', '2026-04-10'),
(758964123, 852469713, 'Allan Sira Ulo', 784512963, 968574123, 'basta motor', 'Expired', '2025-11-02');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `login`
--
ALTER TABLE `login`
  ADD UNIQUE KEY `username` (`username`,`password`);

--
-- Indexes for table `vehicle_drivers`
--
ALTER TABLE `vehicle_drivers`
  ADD UNIQUE KEY `plateNo` (`plateNo`,`bodyNo`,`driverName`,`licenseNo`,`crNo`,`vehicleModel`,`status`,`permitExpiry`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
