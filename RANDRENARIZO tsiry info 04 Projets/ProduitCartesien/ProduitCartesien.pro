QT += core
QT -= gui

CONFIG += c++11

TARGET = ProduitCartesien
CONFIG += console
CONFIG -= app_bundle

TEMPLATE = app

SOURCES += main.cpp \
    Arbre.cpp \
    Ensemble.cpp

HEADERS += \
    Arbre.h \
    Ensemble.h
