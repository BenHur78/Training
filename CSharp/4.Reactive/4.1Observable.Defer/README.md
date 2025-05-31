# Introduction 
The purpose is to study Observable.Defer<TValue> Method https://learn.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh229160(v=vs.103)

# What we learned here
- How to convert an array of items into an observable sequence using ToAsyncEnumerable().ToObservable() (System.Linq.Async).
- How to use Observable.Defer<TValue> to create an observable sequence that only creates the underlying observable when subscribed to.