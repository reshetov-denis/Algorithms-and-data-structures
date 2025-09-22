#include <iostream>
#include <string>
#include <vector>
using namespace std;

// Создать структуру, хранящую информацию о заказах, принимаемых
// швейным ателье: номер заказа, заказчик, вид пошива (перечисление),
// дата приема (число, месяц, год как вложенная структура), стоимость
// заказа. Создать несколько таких структур и заполнить их. Вывести на
// экран все заказы, принятые в прошлом месяце. Если таковых нет, то
// выдать сообщение.

enum SewingType { Dress, Suit, Skirt, Trousers, Other };

struct Date {
    int Day;
    int Month;
    int Year;

    Date(int day = 1, int month = 1, int year = 2000)
        : Day(day), Month(month), Year(year) {
    }
};

struct Order {
    int Id;
    string Customer;
    SewingType Type;
    Date AcceptDate;
    double Cost;
};

bool isLeapYear(int year) {
    if (year % 4 == 0) return true;
    if (year % 100 == 0) return false;
    return (year % 400 == 0);
}

int daysInMonth(int month, int year) {
    switch (month) {
    case 2: return isLeapYear(year) ? 29 : 28;
    case 4: case 6: case 9: case 11: return 30;
    default: return 31;
    }
}

int dateToDays(const Date& date) {
    int totalDays = 0;

    for (int year = 1; year < date.Year; year++) {
        totalDays += isLeapYear(year) ? 366 : 365;
    }

    for (int month = 1; month < date.Month; month++) {
        totalDays += daysInMonth(month, date.Year);
    }

    totalDays += date.Day - 1;
    return totalDays;
}

int daysBetweenDates(const Date& date1, const Date& date2) {
    int days1 = dateToDays(date1);
    int days2 = dateToDays(date2);

    return days2 - days1;
}

int main() {
    setlocale(LC_ALL, "RU");

    vector<Order> orders;
    Date today(16, 9, 2025);
    int id = 0;

    while (true) {
        cout << "Создать заказ? (0 - нет, 1 - да): ";
        int isContinue;
        if (!(cin >> isContinue)) break;
        if (isContinue == 0) break;

        cout << "\nЗаказ #" << id + 1 << "\n";

        Order order;
        order.Id = id;
        cin.ignore();

        cout << "Заказчик: ";
        getline(cin, order.Customer);

        cout << "Вид пошива (0 - платье, 1 - костюм, 2 - юбка, 3 - брюки, 4 - другое): ";
        int chosenType;
        cin >> chosenType;
        order.Type = (SewingType)chosenType;

        cout << "Дата приёма (дд мм гггг): ";
        cin >> order.AcceptDate.Day >> order.AcceptDate.Month >> order.AcceptDate.Year;

        cout << "Стоимость заказа: ";
        cin >> order.Cost;

        cout << endl;
        orders.push_back(order);
        id++;
    }

    int ordersLastMonthCounter = 0;
    cout << "\n---ЗАКАЗЫ ЗА ПОСЛЕДНИЙ МЕСЯЦ---\n";

    for (Order order : orders) {
        int daysDifference = daysBetweenDates(order.AcceptDate, today);
        if (daysDifference > 30 || daysDifference < 0) continue;

        cout << "Заказчик: " << order.Customer << endl;
        switch (order.Type) {
        case Dress:
            cout << "Пошив: платье" << endl; break;
        case Suit:
            cout << "Пошив: костюм" << endl; break;
        case Skirt:
            cout << "Пошив: юбка" << endl; break;
        case Trousers:
            cout << "Пошив: брюки" << endl; break;
        case Other:
            cout << "Пошив: другое" << endl; break;
        }
        cout << "Дата: " << order.AcceptDate.Day << "." << order.AcceptDate.Month << "." << order.AcceptDate.Year << endl;
        cout << "Стоимость: " << order.Cost << " руб." << endl << endl;

        ordersLastMonthCounter++;
    }

    if (ordersLastMonthCounter == 0) cout << "Таких заказов нет!\n";

    return 0;
}