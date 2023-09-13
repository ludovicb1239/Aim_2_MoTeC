# Aim_2_MoTeC
![image](https://github.com/ludovicb1239/Aim_2_MoTeC/assets/59945694/f505ae17-38f9-4118-a4ca-edb87030bf27)

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

## Introduction

Program that converts Aim RaceStudio Data to MoTeC i2 Pro Data.

Programmed in VS C#. 

Uses a dll to read from the file.

We invested a significant amount of effort in reverse engineering to decipher Motec's proprietary file format. This was necessary to enable the file to be both written and opened by i2 Pro. If i2 detects even a minor formatting error in the file, it will either refuse to read it or recognize it as a Standard file rather than a Pro file.

## Features

- Reading all of the data contained in an Aim File
- Converting names from Aim standart to MoTeC standart
- Fully open source
- Simple interface
- Sub-folder search: searched all sub folders for files and converts all of them
- Read data preview button before converting
- Choice between processed or raw GPS data (using Aim dll)

## Getting Started

The project build is standalone for Windows 64b. Only download the latest build and extract from zip to run the app.

## Usage

1. Run the application
2. Check sub-folder if you have multiple files to convert
3. Browse for folder or file
4. [optionnal] select output directory
5. [optionnal] Press Read data to preview the data and channel names
6. [optionnal] Add to NameConversion.txt name to be converted
7. Check Rename Channels if you want to rename the channels to those suggested
8. Check Use raw GPS data if you wish to use raw GPS data from file
9. Convert
10. Open converted file in i2 Pro

## Configuration

Feel free to configure NameConversion.txt to use names that should be converted to MoTeC

## Contributing

We welcome contributions from the community to help improve and grow this project. Whether you're interested in fixing a bug, implementing a new feature, or simply improving documentation, your contributions are highly appreciated.

#### Reporting Bugs

If you encounter a bug or issue while using our project, please help us by reporting it. To report a bug, follow these steps:

1. **Check Existing Issues:** Before creating a new issue, please search the issues list to see if the bug has already been reported by someone else.
2. **Create a New Issue:** If the issue doesn't already exist, open a new issue. Be sure to provide a clear and detailed description of the problem. Include information about your environment (e.g., operating system, browser version) and steps to reproduce the issue, if possible.

#### Requesting Features

If you have an idea for a new feature or enhancement, we encourage you to share it with us. To request a new feature, follow these steps:

1. **Check Existing Requests:** First, search the issues list to see if someone else has already requested the feature.
2. **Create a New Feature Request:** If it hasn't been requested before, open a new issue and clearly describe the feature you have in mind. Explain why it would be valuable and how it would benefit users.

#### Code Contributions

If you're interested in contributing code to the project, please follow these guidelines:

1. **Fork the Repository:** Fork the project's repository to your GitHub account.
2. **Create a Branch:** Create a new branch in your forked repository for the specific feature or bug fix you're working on. Use a descriptive name for your branch.
3. **Make Changes:** Make your changes or additions to the codebase.
4. **Test:** Ensure that your changes work as intended and do not introduce new issues. Run any relevant tests.
5. **Commit and Push:** Commit your changes with clear and concise commit messages. Push your branch to your forked repository.
6. **Create a Pull Request:** Open a pull request from your branch to the main repository's main branch. Provide a detailed description of your changes.
7. **Code Review:** Your pull request will be reviewed by maintainers. Be prepared to make changes based on feedback.
8. **Merge:** Once your changes are approved, they will be merged into the main branch.

## License

This project is licensed under the [MIT License](LICENSE).

---
